using Microsoft.AspNetCore.Mvc;
using ReastosMenu.Models;
using ReastosMenu.Services;
using System.Diagnostics;

namespace ReastosMenu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMenuService _menuService;
        private readonly IMenuImageService _menuImageService;
        private readonly IBannerService _bannerService;
        private readonly IBookingService _bookingService;

        public HomeController(ILogger<HomeController> logger, IMenuService menuService, IMenuImageService menuImageService, IBannerService bannerService, IBookingService bookingService)
        {
            _logger = logger;
            _menuService = menuService;
            _menuImageService = menuImageService;
            _bannerService = bannerService;
            _bookingService=bookingService;
        }

        public IActionResult Index()
        {
            var breakfastMenu = _menuService.GetBreakFast();
            var lunchMenu = _menuService.GetLunch();
            var dinnerMenu = _menuService.GetDinner();

            // Map Breakfast menu entity to its model version

            var breakfastMenuModelList = breakfastMenu.Select(menu => new MenuModel
            {
                Id = menu.Id,
                Title = menu.Title,
                Description = menu.Description,
                Price = menu.Price,
                ImagesUrl = menu.ImagesUrl,
            }).ToList();

            // Map Lunch menu entity to its model version

            var lunchMenuModelList = lunchMenu.Select(menu => new MenuModel
            {
                Id = menu.Id,
                Title = menu.Title,
                Description = menu.Description,
                Price = menu.Price,
                ImagesUrl = menu.ImagesUrl,
            }).ToList();

            // Map Dinner menu entity to its model version

            var dinnerMenuModelList = dinnerMenu.Select(menu => new MenuModel
            {
                Id = menu.Id,
                Title = menu.Title,
                Description = menu.Description,
                Price = menu.Price,
                ImagesUrl = menu.ImagesUrl,
            }).ToList();

            // Get Banner

            var banner = _bannerService.GetALLBanner();

            var bannerModelList = banner.Select(banner => new BannerModel
            {
                Title1 = banner.Title1,
                Title2 = banner.Title2,
                Description = banner.Description,
                //ImagePaths  =  banner.BannerImages.Select(b => b.ImagePath).ToList(),
            }).ToList();

            var landingPageModel = new LandingPageModel
            {
                BreakFastMenu = breakfastMenuModelList,
                LuchMenu = lunchMenuModelList,
                DinnerMenu = dinnerMenuModelList,
                Banners = bannerModelList
            };

            return View(landingPageModel);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Menu()
        {
            var breakfastMenu = _menuService.GetBreakFast();
            var lunchMenu = _menuService.GetLunch();
            var dinnerMenu = _menuService.GetDinner();

            // Map Breakfast menu entity to its model version

            var breakfastMenuModelList = breakfastMenu.Select(menu => new MenuModel
            {
                Id = menu.Id,
                Title = menu.Title,
                Description = menu.Description,
                Price = menu.Price,
                ImagesUrl = menu.ImagesUrl,
            }).ToList();

            // Map Lunch menu entity to its model version

            var lunchMenuModelList = lunchMenu.Select(menu => new MenuModel
            {
                Id = menu.Id,
                Title = menu.Title,
                Description = menu.Description,
                Price = menu.Price,
                ImagesUrl= menu.ImagesUrl,
            }).ToList();

            // Map Dinner menu entity to its model version

            var dinnertMenuModelList = dinnerMenu.Select(menu => new MenuModel
            {
                Id = menu.Id,
                Title = menu.Title,
                Description = menu.Description,
                Price = menu.Price,
                ImagesUrl=(menu.ImagesUrl),
            }).ToList();

            // Get Banner

            var banner = _bannerService.GetALLBanner();

            var bannerModelList = banner.Select(banner => new BannerModel
            {
                Title1 = banner.Title1,
                Title2 = banner.Title2,
                Description = banner.Description,
                //ImagePaths  =  banner.BannerImages.Select(b => b.ImagePath).ToList(),
            }).ToList();

            var landingPageModel = new LandingPageModel
            {
                BreakFastMenu = breakfastMenuModelList,
                LuchMenu = lunchMenuModelList,
                DinnerMenu = dinnertMenuModelList,
                Banners = bannerModelList
            };

            return View(landingPageModel);
        }

        public IActionResult Book()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(BookingModel bookingModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookingModel);
            }

            await _bookingService.AddBooking(bookingModel);

            return RedirectToAction("Book");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}