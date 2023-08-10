using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {
        // Dependency Injection
        private readonly RepositoryContext _context;

        public ProductController(RepositoryContext context)
        {
            _context = context;
        }
        //
        public IActionResult Index()
        {
            var model =_context.Products.ToList(); // DI yaptığımız için direkt _context yazarak erişebildik
            return View(model);
        }
        public IActionResult Get(int id)
        {
            Product product=_context.Products.First(x=>x.ProductId.Equals(id));
            return View(product);
        }
    }
}