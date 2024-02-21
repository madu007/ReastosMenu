using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReastosMenu.Data;
using ReastosMenu.Models;
using ReastosMenu.Services;
using System.ComponentModel.DataAnnotations;
using ReastosMenu.Entities;

namespace ReastosMenu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MenuController : Controller
    {
        private readonly ILogger<MenuController> _logger;
        private readonly IMenuService _menuService;
        private readonly IMenuImageService _menuImageService;
        private readonly ICategoryService _categoryService;
        private readonly RestosMenuDbContext restosMenuDbContext;

        public MenuController (ILogger<MenuController> logger, IMenuService menuService, IMenuImageService menuImageService, ICategoryService categoryService, RestosMenuDbContext restosMenuDbContext)
        {
            _logger = logger;
            _menuService = menuService;
            _menuImageService = menuImageService;
            _categoryService = categoryService;
            this.restosMenuDbContext=restosMenuDbContext;
        }
        public IActionResult Index()
        {
            var menuList = _menuService.GetAllMenus();

            var menuModelList = menuList.Select(menu => new MenuModel
            {
                Title = menu.Title,
                Description = menu.Description,
                Price = menu.Price,
                Id = menu.Id,
                ImagesUrl = menu.ImagesUrl,
            }).ToList();

            return View(menuModelList);
        }
        [HttpGet]
        public IActionResult AddMenu()
        {
            var categories = _categoryService.GetAllCategory();
            var menuModel = new MenuModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title,
                })
            };
            return View(menuModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMenu([Bind(include: "Id, Title, Description, Price, ImagesUrl, SelectedCategoryId")] MenuModel menuModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(menuModel);
            //}

            _menuService.AddMenu(menuModel);
            TempData["message"] = "Menu details saved Successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditMenu(int id)
        {

           var menu = _menuService.EditMenu(id);

            return View(menu);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMenu(MenuModel menuModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(menuModel);
            //}

            _menuService.UpdateMenu(menuModel);
            TempData["message"] = "Menu details Updated Successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            await _menuService.DeleteMenu(id);
            TempData["message"] = "Menu details Deleted Successfully";

            return RedirectToAction("Index", "Menu");
        }
    }
}
