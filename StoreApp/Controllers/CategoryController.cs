using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace StoreApp.Controllers
{
    public class CategoryController:Controller
    {
        private  IRepositoryManager _manager;

        public CategoryController(IRepositoryManager manager)
        {
            _manager = manager;
        }
        public IActionResult Index()
        {
            ViewData["Title"]="Category";
            var model = _manager.Category.FindAll(false);
            return View(model);
        }
    }
}
