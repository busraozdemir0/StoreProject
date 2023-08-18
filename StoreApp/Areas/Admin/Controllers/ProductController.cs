using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index([FromQuery] ProductRequestParameters p)
        {
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
       
        private SelectList GetCategoriesSelectList() // Kategorilerin listbox'da listelenmesi için fonksiyon
        {
            return new SelectList(_manager.CategoryService.GetAllCategories(false),
            "CategoryId",
            "CategoryName","1");  // 1 dediğimiz varsayılan seçili gelmesi için           

        }
        public IActionResult Create()
        {
            ViewBag.Categories=GetCategoriesSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Formun doğrulanması için
        public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto,IFormFile file) // ürün bilgisi formdan gelecek
        {
            if (ModelState.IsValid)
            {
                // file operations
                string path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images", file.FileName);               
                using(var stream=new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                productDto.ImageUrl=String.Concat("/images/",file.FileName);
                _manager.ProductService.CreateProduct(productDto);
                TempData["success"] = $"{productDto.ProductName} has been created."; // Kullanıcıya ürün eklendiğine dair bildirim(feedback)
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        public IActionResult Update([FromRoute(Name = "id")] int id)
        {
            ViewBag.Categories=GetCategoriesSelectList();
            var model = _manager.ProductService.GetOneProductForUpdate(id, false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm]ProductDtoForUpdate productDto,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                // file operations
                string path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images", file.FileName);               
                using(var stream=new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                productDto.ImageUrl=String.Concat("/images/",file.FileName);
                _manager.ProductService.UpdateOneProduct(productDto);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete([FromRoute(Name = "id")] int id)
        {
            _manager.ProductService.DeleteOneProduct(id);
            TempData["danger"]="The product has been removed.";
            return RedirectToAction("Index");
        }
    }
}
