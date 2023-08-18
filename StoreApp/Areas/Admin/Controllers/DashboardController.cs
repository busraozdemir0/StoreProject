using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")] //Area'larla çalıştığımız zaman bu şekilde belirtmemiz gerekir
    [Authorize(Roles = "Admin")]
    public class DashboardController:Controller
    {
        public IActionResult Index()
        {
            TempData["info"]=$"Welcome back, {DateTime.Now.ToShortTimeString()}"; // Kullanıcının admin sayfasına başarıyla giriş yaptığını ve o anki saati gösteren bildirim.
            return View();
        }
    }
}