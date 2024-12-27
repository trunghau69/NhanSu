using Microsoft.AspNetCore.Mvc;

namespace Employee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChamCongController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
