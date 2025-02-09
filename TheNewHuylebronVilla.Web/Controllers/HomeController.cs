
using System . Diagnostics ;
using Microsoft . AspNetCore . Mvc ;
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
        public IActionResult Index(HomeVM homeVM)
        {
            homeVM.VillaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity");
            foreach(var villa in homeVM.VillaList)
            {
                if (villa.Id % 2 == 0)
                {
                    villa.IsAvailable = false;
                }
            } 
            return View(homeVM);
        }

      

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View( );
        }
    }
}
