using Microsoft.AspNetCore.Mvc;

namespace StoreApp.AddControllersWithViews{
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"]="Welcome";
            return View();
        }
    }
}