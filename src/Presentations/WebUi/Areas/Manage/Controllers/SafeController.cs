using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.WebUi.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SafeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}