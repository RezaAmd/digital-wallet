using Microsoft.AspNetCore.Mvc;

namespace WebUi.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DepositController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}