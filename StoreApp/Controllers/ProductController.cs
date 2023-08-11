using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {
        // Dependency Injection
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }
        //
        public IActionResult Index()
        {
            var model =_manager.ProductService.GetAllProducts(false); // DI yaptığımız için direkt _manager yazarak erişebildik
            return View(model);
        }
        public IActionResult Get([FromRoute(Name ="id")]int id)
        {
          var model=_manager.ProductService.GetOneProduct(id,false);
          return View(model);
        }
    }
}