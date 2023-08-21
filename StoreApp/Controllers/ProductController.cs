using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using Entities.RequestParameters;
using StoreApp.Models;

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
            ViewData["Title"]="Product";
            var products =_manager.ProductService.GetAllProductsWithDetails(p); // DI yaptığımız için direkt _manager yazarak erişebildik
            var pagination=new Pagination()
            {
                CurrentPage=p.PageNumber,
                ItemsPerPage=p.PageSize,
                TotalItems=_manager.ProductService.GetAllProducts(false).Count()
            };
            return View(new ProductListViewModel(){
                Products = products,
                Pagination = pagination
            });
        }
        public IActionResult Get([FromRoute(Name ="id")]int id)
        {          
          var model=_manager.ProductService.GetOneProduct(id,false);
          ViewData["Title"]=model?.ProductName;
          return View(model);
        }
    }
}