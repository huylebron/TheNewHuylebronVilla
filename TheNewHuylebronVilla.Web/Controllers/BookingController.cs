using System . Security . Claims ;
using Microsoft . AspNetCore . Authorization ;
using Microsoft . AspNetCore . Mvc ;
using NewHuylebronVilla . Application . Common . Interface ;
using NewHuylebronVilla . Domain . Entities ;
using Stripe . Checkout ;
using Claim = NewHuylebronVilla . Application . Common . Division . Claim ;

namespace TheNewHuylebronVilla.Web.Controllers ;

public class BookingController : Controller
{
  private readonly IUnitOfWork _unitOfWork;
public BookingController(IUnitOfWork unitOfWork)
{
    _unitOfWork = unitOfWork;
}
[Authorize]
public IActionResult Index()
{
    return View();
}

[Authorize]
public IActionResult FinalizeBooking(int villaId, DateOnly checkInDate, int nights)
{
    var claimsIdentity = (ClaimsIdentity)User.Identity;
    var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

    ApplicationUser user = _unitOfWork.User.Get(u => u.Id == userId);

    Booking booking = new() 
    {
        VillaId = villaId,
        Villa = _unitOfWork.Villa.Get(u=>u.Id==villaId, includeProperties:"VillaAmenity"),
        CheckInDate = checkInDate,
        Nights = nights,
        CheckOutDate = checkInDate.AddDays(nights),
        UserId = userId,
        Phone=user.PhoneNumber,
        Email=user.Email,
        Name=user.Name
    };
    booking.TotalCost = booking.Villa.Price * nights;
    return View(booking);
}

[Authorize]
[HttpPost]
public IActionResult FinalizeBooking(Booking booking)
{
    var villa = _unitOfWork.Villa.Get(u => u.Id == booking.VillaId);
    booking.TotalCost = villa.Price * booking.Nights;

    booking.Status=Claim.StatusPending;
    booking.BookingDate = DateTime.Now;
    var villaNumbersList = _unitOfWork.VillaNumber.GetAll().ToList();
    var bookedVillas = _unitOfWork.Booking.GetAll(u => u.Status == Claim.StatusApproved ||
                                                       u.Status == Claim.StatusCheckedIn).ToList();


 
    int roomAvailable = Claim.VillaRoomsAvailable_Count
        (villa.Id, villaNumbersList, booking.CheckInDate, booking.Nights, bookedVillas);

    if (roomAvailable == 0)
    {
        TempData["error"] = "Room has been sold out!";
        //no rooms available
        return RedirectToAction(nameof(FinalizeBooking), new
        {
            villaId = booking.VillaId,
            checkInDate = booking.CheckInDate,
            nights = booking.Nights
        });
    }

    _unitOfWork.Booking.Add(booking);
    _unitOfWork.Save();

    var domain = Request.Scheme+"://"+Request.Host.Value+"/";
    var options = new SessionCreateOptions
    {
        LineItems = new List<SessionLineItemOptions>(),
        Mode = "payment",
        SuccessUrl = domain + $"booking/BookingConfirmation?bookingId={booking.Id}",
        CancelUrl = domain + $"booking/FinalizeBooking?villaId={booking.VillaId}&checkInDate={booking.CheckInDate}&nights={booking.Nights}",
    };


    options.LineItems.Add(new SessionLineItemOptions
    {
        PriceData = new SessionLineItemPriceDataOptions
        {
            UnitAmount = (long)(booking.TotalCost * 100),
            Currency = "usd",
            ProductData = new SessionLineItemPriceDataProductDataOptions
            {
                Name = villa.Name
                //Images = new List<string> { domain + villa.ImageUrl },
            },
        },
        Quantity = 1,
    });

   
    var service = new SessionService();
    Session session = service.Create(options);

    _unitOfWork.Booking.UpdateStripePaymentID(booking.Id, session.Id, session.PaymentIntentId);
    _unitOfWork.Save();
    Response.Headers.Add("Location", session.Url);
    return new StatusCodeResult(303);

}

[Authorize]
public IActionResult BookingConfirmation(int bookingId)
{
    Booking bookingFromDb = _unitOfWork.Booking.Get(u => u.Id == bookingId,
        includeProperties: "User,Villa");

    if (bookingFromDb.Status == Claim.StatusPending)
    {
        //this is a pending order, we need to confirm if payment was successful

        var service = new SessionService();
        Session session = service.Get(bookingFromDb.StripeSessionId);

        if (session.PaymentStatus == "paid")
        {
            _unitOfWork.Booking.UpdateStatus(bookingFromDb.Id, Claim.StatusApproved,0);
            _unitOfWork.Booking.UpdateStripePaymentID(bookingFromDb.Id,session.Id,session.PaymentIntentId);
            _unitOfWork.Save();
        }
    }

    return View(bookingId);
}
[Authorize]
public IActionResult BookingDetails(int bookingId)
{
    Booking bookingFromDb = _unitOfWork.Booking.Get(u => u.Id == bookingId,
        includeProperties: "User,Villa");
    if(bookingFromDb.VillaNumber==0 && bookingFromDb.Status == Claim.StatusApproved)
    {
        var availableVillaNumber = AssignAvailableVillaNumberByVilla(bookingFromDb.VillaId);

        bookingFromDb.VillaNumbers = _unitOfWork.VillaNumber.GetAll(u => u.VillaId == bookingFromDb.VillaId
                                                                         && availableVillaNumber.Any(x => x == u.Villa_Number)).ToList();
    }

    return View(bookingFromDb);
}

[HttpPost]
[Authorize(Roles = Claim.Role_Admin)]
public IActionResult CheckIn(Booking booking)
{
    _unitOfWork.Booking.UpdateStatus(booking.Id, Claim.StatusCheckedIn, booking.VillaNumber);
    _unitOfWork.Save();
    TempData["Success"] = "Booking Updated Successfully.";
    return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
}

[HttpPost]
[Authorize(Roles = Claim.Role_Admin)]
public IActionResult CheckOut(Booking booking)
{
    _unitOfWork.Booking.UpdateStatus(booking.Id, Claim.StatusCompleted , booking.VillaNumber);
    _unitOfWork.Save();
    TempData["Success"] = "Booking Completed Successfully.";
    return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
}

[HttpPost]
[Authorize(Roles = Claim.Role_Admin)]
public IActionResult CancelBooking(Booking booking)
{
    _unitOfWork.Booking.UpdateStatus(booking.Id, Claim.StatusCancelled, 0);
    _unitOfWork.Save();
    TempData["Success"] = "Booking Cancelled Successfully.";
    return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
}



private List<int> AssignAvailableVillaNumberByVilla(int villaId)
{
    List<int> availableVillaNumbers = new();

    var villaNumbers = _unitOfWork.VillaNumber.GetAll(u => u.VillaId == villaId);

    var checkedInVilla = _unitOfWork.Booking.GetAll(u=>u.VillaId==villaId && u.Status==Claim.StatusCheckedIn)
                                    .Select(u=>u.VillaNumber);    

    foreach(var villaNumber in villaNumbers)
    {
        if (!checkedInVilla.Contains(villaNumber.Villa_Number))
        {
            availableVillaNumbers.Add(villaNumber.Villa_Number);
        }
    }
    return availableVillaNumbers;
}


#region API Calls
[HttpGet]
[Authorize]
public IActionResult GetAll( string status)
{
    IEnumerable<Booking> objBookings;

    if (User.IsInRole(Claim.Role_Admin))
    {
        objBookings = _unitOfWork.Booking.GetAll(includeProperties: "User,Villa");
    }
    else
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        objBookings = _unitOfWork.Booking
            .GetAll(u => u.UserId == userId, includeProperties: "User,Villa");
    }
    if (!string.IsNullOrEmpty(status))
    {
        objBookings  = objBookings.Where(u => u.Status.ToLower().Equals(status.ToLower()));
    }
    return Json(new { data = objBookings });
}

#endregion
   
}