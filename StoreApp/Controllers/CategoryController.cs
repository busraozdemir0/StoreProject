using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

namespace StoreApp.Controllers
{
    public class CategoryController:Controller
    {
        private  IRepositoryManager _manager;
        private readonly IServiceManager _serviceManager;

        public CategoryController(IRepositoryManager manager, IServiceManager serviceManager)
        {
            _manager = manager;
            _serviceManager = serviceManager;
        }
        public IActionResult Index()
        {
            ViewData["Title"]="Category";
            var model = _manager.Category.FindAll(false);
            return View(model);
        }
    }
}
