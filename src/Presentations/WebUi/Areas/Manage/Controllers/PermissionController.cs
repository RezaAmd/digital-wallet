using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.WebUi.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PermissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
