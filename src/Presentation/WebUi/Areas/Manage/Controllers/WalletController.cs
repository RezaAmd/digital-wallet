﻿using Microsoft.AspNetCore.Mvc;

namespace WebUi.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class WalletController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}