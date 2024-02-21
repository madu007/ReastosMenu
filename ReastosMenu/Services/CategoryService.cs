using Microsoft.EntityFrameworkCore;
using ReastosMenu.Data;
using ReastosMenu.Entities;
using ReastosMenu.Models;
using System.ComponentModel.DataAnnotations;

namespace ReastosMenu.Services
{
    public interface ICategoryService
    {
        List<Category> GetAllCategory();
        Category GetCategory(int id);
        Category AddCategory(CategoryModel categoryModel);
        CategoryModel EditCategory(int id);
        Category EditCategory(CategoryModel categoryModel);
        Task DeleteCategory(int id);
    }
    public class CategoryService : ICategoryService
    {
        private readonly RestosMenuDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public CategoryService(RestosMenuDbContext context, IWebHostEnvironment environment) 
        {
            _context = context;
            _environment = environment;
        }

        public List<Category> GetAllCategory()
        {
            var categoryList = _context.Categories
                .OrderBy(c => c.Id)
                .ToList();

            return categoryList;
        }


        public Category GetCategory(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                throw new ValidationException("Id is required");
            }

            return category;
        }

        public  Category AddCategory(CategoryModel categoryModel)
        {   
            try
            {

                var categoryEntity = new Category
                {
                    Title = categoryModel.Title,
                    Description = categoryModel.Description
                };

                _context.Categories.Add(categoryEntity);
                _context.SaveChanges();

                return categoryEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CategoryModel EditCategory(int id)
        {
            try
            {
                var category = _context.Categories.Find(id);
                if (category == null)
                {
                    throw new ValidationException("Id is required");
                }

                var categoryModel = new CategoryModel
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description
            };

            return categoryModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Category EditCategory(CategoryModel categoryModel)
        {
            var category = _context.Categories.Find(categoryModel.Id);
            if (category == null)
            {
                throw new ValidationException("Id is required");
            }

            category.Title = categoryModel.Title;
            category.Description = categoryModel.Description;

            _context.SaveChanges();

            return category;
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new ValidationException("Id is required");
            }

           _context.Categories.Remove(category);
           await  _context.SaveChangesAsync();            
        }
    }
}
