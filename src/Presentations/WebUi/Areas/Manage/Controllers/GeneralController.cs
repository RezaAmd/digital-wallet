using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.WebUi.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class GeneralController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}