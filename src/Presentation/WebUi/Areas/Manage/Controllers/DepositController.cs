using Microsoft.AspNetCore.Mvc;

namespace WebUi.Areas.Manage.Controllers
{
    public class DepositController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}