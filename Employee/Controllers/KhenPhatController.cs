using Microsoft.AspNetCore.Mvc;

namespace Employee.Controllers
{
    public class KhenPhatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
