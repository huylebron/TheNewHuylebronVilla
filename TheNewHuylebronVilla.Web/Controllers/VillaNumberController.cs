using Microsoft . AspNetCore . Mvc ;
using Microsoft . AspNetCore . Mvc . Rendering ;
using Microsoft . EntityFrameworkCore ;
using NewHuylebronVilla . Domain . Entities ;
using NewHuylebronVilla . Infrastructure . Data ;
using TheNewHuylebronVilla . Web . ViewModels ;

namespace TheNewHuylebronVilla.Web.Controllers ;

public class VillaNumberController : Controller
{
    private readonly ApplicationDbContext _db;
    
    public VillaNumberController(ApplicationDbContext db)
    {
        _db = db;
    }
    // GET
    public IActionResult Index ( ) {
        
        var villaNumbers = _db.VillaNumbers.Include(u => u.Villa).ToList();
        return View(villaNumbers);
    }
    
    public IActionResult Create()
    {
       /* IEnumerable<SelectListItem> list = _db.Villas.ToList().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString()
        });
        ViewData["VillaList"] = list;
        return View();*/
       
       VillaNumberVM villaNumberVM = new()
       {
           VillaList = _db.Villas.ToList().Select(u => new SelectListItem
           {
               Text = u.Name,
               Value = u.Id.ToString()
           })
       };
       return View(villaNumberVM);
    }

    [HttpPost]
    /*public IActionResult Create(VillaNumber obj)
    {
        
        if (ModelState.IsValid)
        {
            _db.VillaNumbers.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "The villa Number has been created successfully.";
            return RedirectToAction("Index");
        }
        return View();
    }*/
    public IActionResult Create(VillaNumberVM obj)
    {
        //ModelState.Remove("Villa");

        bool roomNumberExists = _db.VillaNumbers.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);

        if (ModelState.IsValid && !roomNumberExists)
        {
            _db.VillaNumbers.Add(obj.VillaNumber);
            _db.SaveChanges();
            TempData["success"] = "The villa Number has been created successfully.";
            return RedirectToAction("Index");
        }

        if (roomNumberExists)
        {
            TempData["error"] = "The villa Number already exists.";
        }
        obj.VillaList = _db.Villas.ToList().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString()
        });
        return View(obj);
    }

    public IActionResult Update(int villaNumberId)
    {
        VillaNumberVM villaNumberVM = new()
        {
            VillaList = _db.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
            VillaNumber = _db.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)
        };
        if (villaNumberVM.VillaNumber == null)
        {
            return RedirectToAction("Error", "Home");
        }
        return View(villaNumberVM);
    }


    [HttpPost]
    public IActionResult Update(VillaNumberVM villaNumberVM)
    {

        if (ModelState.IsValid)
        {
            _db.VillaNumbers.Update(villaNumberVM.VillaNumber);
            _db.SaveChanges();
            TempData["success"] = "The villa Number has been updated successfully.";
            return RedirectToAction("Index");
        }

        villaNumberVM.VillaList = _db.Villas.ToList().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString()
        });
        return View(villaNumberVM);
    }



   
    public IActionResult Delete(int villaNumberId)
    {
        VillaNumberVM villaNumberVM = new()
        {
            VillaList = _db.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
            VillaNumber = _db.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)
        };
        if (villaNumberVM.VillaNumber == null)
        {
            return RedirectToAction("Error", "Home");
        }
        return View(villaNumberVM);
    }


    [HttpPost]
    public IActionResult Delete(VillaNumberVM villaNumberVM)
    {
        VillaNumber? objFromDb = _db.VillaNumbers
                                    .FirstOrDefault(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);
        if (objFromDb is not null)
        {
            _db.VillaNumbers.Remove(objFromDb);
            _db.SaveChanges();
            TempData["success"] = "The villa number has been deleted successfully.";
            return RedirectToAction("Index");
        }
        TempData["error"] = "The villa number could not be deleted.";
        return View();
    }
    
}