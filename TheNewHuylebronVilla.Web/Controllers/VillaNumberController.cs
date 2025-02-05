using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft . EntityFrameworkCore ;
using NewHuylebronVilla.Domain.Entities;
using NewHuylebronVilla.Infrastructure.Data;
using TheNewHuylebronVilla . Web . ViewModels ;

namespace TheNewHuylebronVilla.Web.Controllers

{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;


        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var villaNumbers = _db.VillaNumbers.Include(u => u.Villa).ToList();
            return View(villaNumbers);
        }


        public IActionResult Create() {
            VillaNumberVM villaNumberVM = new ( )
            {
              
                VillaList = _db.Villas.ToList().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
           

            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {
           // ModelState.Remove("Villa");
           bool VillaNumberExists = _db.VillaNumbers.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
           
            if (ModelState.IsValid && !VillaNumberExists)
            {
                _db.VillaNumbers.Add(obj.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = " created successfully.";
                return RedirectToAction("Index", "VillaNumber");
            }
            
                if (VillaNumberExists)
                {
                    TempData["error"] = "Villa Number already exists";
                }
            obj.VillaList = _db.Villas.ToList().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View(obj);
        }
    }
}