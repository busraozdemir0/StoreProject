using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repositories;
using Repositories.Contracts;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {
        // Dependency Injection
        private readonly IRepositoryManager _manager;

        public ProductController(IRepositoryManager manager)
        {
            _manager = manager;
        }
        //
        public IActionResult Index()
        {
            var model =_manager.Product.GetAllProducts(false); // DI yaptığımız için direkt _manager yazarak erişebildik
            return View(model);
        }
        public IActionResult Get(int id)
        {
          var model=_manager.Product.GetOneProduct(id,false);
          return View(model);
        }
    }
}