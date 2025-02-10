
using System . Diagnostics ;
using Microsoft . AspNetCore . Mvc ;
using NewHuylebronVilla . Application . Common . Division ;
using NewHuylebronVilla . Application . Common . Interface ;
using TheNewHuylebronVilla . Web . Models ;
using TheNewHuylebronVilla . Web . ViewModels ;

namespace TheNewHuylebronVilla.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity"),
                Nights=1,
                CheckInDate =DateOnly.FromDateTime(DateTime.Now),
            };
            return View(homeVM);
        }

        
        [HttpPost]
        public IActionResult GetVillasByDate(int nights, DateOnly checkInDate) 
        {
            var villaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity").ToList();
            var villaNumbersList = _unitOfWork.VillaNumber.GetAll().ToList();
            var bookedVillas = _unitOfWork.Booking.GetAll(u => u.Status == Claim.StatusApproved ||
                                                               u.Status == Claim.StatusCheckedIn).ToList();


            foreach (var villa in villaList)
            {
                int roomAvailable = Claim.VillaRoomsAvailable_Count
                    (villa.Id, villaNumbersList, checkInDate, nights, bookedVillas);

                villa.IsAvailable = roomAvailable > 0 ? true : false;
            }
            HomeVM homeVM = new()
            {
                CheckInDate = checkInDate,
                VillaList = villaList,
                Nights = nights
            };

            return PartialView("_VillaList",homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
