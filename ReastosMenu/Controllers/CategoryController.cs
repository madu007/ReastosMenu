using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReastosMenu.Models;
using ReastosMenu.Services;

namespace ReastosMenu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService) 
        {
            _logger = logger;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            try
            {
                var categoryList = _categoryService.GetAllCategory();

                var categoryModelList = categoryList.Select(category => new CategoryModel
                {
                    Title = category.Title,
                    Description = category.Description,
                    Id = category.Id
                }).ToList();

                return View(categoryModelList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            CategoryModel category = new CategoryModel();
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(CategoryModel categoryModel)
        {
            if(!ModelState.IsValid)
            {
                return View(categoryModel);
            }

             _categoryService.AddCategory(categoryModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
           var category = _categoryService.EditCategory(id);

            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(CategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryModel);
            }

            _categoryService.EditCategory(categoryModel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
          await  _categoryService.DeleteCategory(id);

            return RedirectToAction("Index", "Category");
        }
    }
}
