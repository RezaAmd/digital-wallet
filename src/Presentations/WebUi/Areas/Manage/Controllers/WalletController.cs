using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.WebUi.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class WalletController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}