using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReastosMenu.Data;
using ReastosMenu.Entities;
using ReastosMenu.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ReastosMenu.Services
{
    public interface IMenuService
    {
        List<Menu> GetAllMenus();
        Menu GetMenuById(int id);
        Menu AddMenu(MenuModel menuModel);
        List<Menu> GetBreakFast();
        List<Menu> GetLunch();
        List<Menu> GetDinner();
        MenuModel EditMenu(int id);
        Menu UpdateMenu(MenuModel menu);
        Task DeleteMenu(int id);
    }
    public class MenuService : IMenuService
    {
        private readonly RestosMenuDbContext _context;

        public MenuService(RestosMenuDbContext context)
        {
            _context = context;
        }

        public List<Menu> GetAllMenus()
        {
            var menuList = _context.Menus
                .OrderBy(m => m.Id)
                .ToList();

            return menuList;
        }

        public Menu GetMenuById(int id)
        {
            var menu = _context.Menus.Find(id);

            if (menu == null)
            {
                throw new ValidationException("Id is required");
            }

            return menu;
        }


        public Menu AddMenu(MenuModel menuModel)
        {
            
            // Save Menu to database
            var MenuEntity = new Menu
            {
                Title = menuModel.Title,
                Description = menuModel.Description,
                Price = menuModel.Price,
                ImagesUrl = menuModel.ImagesUrl,
                Category = _context.Categories.Find(menuModel.SelectedCategoryId)
            };

             _context.Menus.AddAsync(MenuEntity);
             _context.SaveChangesAsync();
            return MenuEntity;
        }

        public List<Menu> GetBreakFast()
        {
            var breakFast = _context.Menus
                .Where(m => m.Category.Title == "Breakfast")
                .OrderBy(m => m.Id)
                .ToList();

            return breakFast;
        }

        public List<Menu> GetLunch()
        {
            var lunch = _context.Menus
                .Where(m => m.Category.Title == "Lunch")
                .OrderBy(m => m.Id)
                .ToList();

            return lunch;
        }

        public List<Menu> GetDinner()
        {
            var dinner = _context.Menus
                .Where(m => m.Category.Title == "Dinner")
                .OrderBy(m => m.Id)
                .ToList();

            return dinner;
        }


        public MenuModel EditMenu(int id)
        {
            var menu = _context.Menus.Find(id);

            if (menu == null)
            {
                throw new ValidationException("Id is not found!");
            }

            var categories = _context.Categories.ToList();

            var menuModel = new MenuModel
            {
                Id = menu.Id,
                Title = menu.Title,
                Description = menu.Description,
                Price = menu.Price,
                ImagesUrl = menu.ImagesUrl,
                SelectedCategoryId = menu.Category.Id,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title,
                }),
            };

            return menuModel;
        }


        public Menu UpdateMenu(MenuModel menu)
        {
           var existingMenu = _context.Menus.Find(menu.Id);
            if (existingMenu == null)
            {
                throw new ValidationException("Id is not found!");
            }

            var categoryEntity = _context.Categories.Find(menu.SelectedCategoryId);
            if (categoryEntity == null)
            {
                throw new ValidationException("Selected Category is not found");
            }

            existingMenu.Title = menu.Title;
            existingMenu.Description = menu.Description;
            existingMenu.Price = menu.Price;
            existingMenu.ImagesUrl = menu.ImagesUrl;
            existingMenu.Category = categoryEntity;

            _context.SaveChanges();
            return existingMenu;
        }


        public async Task DeleteMenu(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                throw new ValidationException("Id is not found");
            }

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
        }
    }
}
