using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(IServiceManager manager, RoleManager<IdentityRole> roleManager)
        {
            _manager = manager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Role";
            return View(_manager.AuthService.Roles);
        }

        public IActionResult Update([FromRoute(Name = "id")] string id)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            RoleDtoForUpdate roleDto = new RoleDtoForUpdate { Id = values.Id, Name = values.Name };
            return View(roleDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(RoleDtoForUpdate roleDto)
        {
            var values = _roleManager.Roles.Where(x => x.Id == roleDto.Id).FirstOrDefault();
            values.Name = roleDto.Name;
            var result = await _roleManager.UpdateAsync(values);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(roleDto);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleDtoForCreation roleDto)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole() { Name = roleDto.Name, };
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(roleDto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] string id)
        {
            var values=_roleManager.Roles.Where(r=>r.Id.Equals(id)).FirstOrDefault();
            var result=await _roleManager.DeleteAsync(values);
            TempData["danger"]="The product has been removed.";
            if(result.Succeeded)
            {
                return RedirectToAction("Index");         
            }

            return View();
        }
    }
}
