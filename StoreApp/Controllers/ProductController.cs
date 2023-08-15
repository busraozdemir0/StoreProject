using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using Entities.RequestParameters;

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
        public IActionResult Index(ProductRequestParameters p)
        {
            var model =_manager.ProductService.GetAllProductsWithDetails(p); // DI yaptığımız için direkt _manager yazarak erişebildik
            return View(model);
        }
        public IActionResult Get([FromRoute(Name ="id")]int id)
        {
          var model=_manager.ProductService.GetOneProduct(id,false);
          return View(model);
        }
    }
}