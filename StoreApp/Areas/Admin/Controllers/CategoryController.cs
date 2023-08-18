using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IServiceManager _manager;

        public CategoryController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Category";
            var categories = _manager.CategoryService.GetAllCategories(false);
            return View(categories);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Category";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] CategoryDtoForInsertion categoryDto)
        {
            _manager.CategoryService.CreateCategory(categoryDto);
            TempData["success"] = $"{categoryDto.CategoryName} has been created.";
            return RedirectToAction("Index", "Category");
        }

        public IActionResult Update([FromRoute(Name = "id")] int id)
        {
            var model = _manager.CategoryService.GetOneCategoryForUpdate(id, false);
            ViewData["Title"] = model?.CategoryName;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] CategoryDtoForUpdate categoryDto)
        {
            _manager.CategoryService.UpdateOneCategory(categoryDto);
            return RedirectToAction("Index", "Category");
        }

        public IActionResult Delete([FromRoute(Name = "id")] int id) // Categorilere ait ürünler eklenmişse o kategoriyi sildiğimizde hata verecektir.
        {
            _manager.CategoryService.DeleteOneCategory(id);
            TempData["danger"]="The category has been removed.";
            return RedirectToAction("Index");
        }
    }
}
