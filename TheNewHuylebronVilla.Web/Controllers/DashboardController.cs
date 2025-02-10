using Microsoft . AspNetCore . Mvc ;

namespace TheNewHuylebronVilla.Web.Controllers ;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}