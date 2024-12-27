using Microsoft.AspNetCore.Mvc;

namespace Employee.Controllers
{
    public class NhanLuongController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
