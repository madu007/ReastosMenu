using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReastosMenu.Entities.Identity;
using ReastosMenu.Models;
using ReastosMenu.Services;
using System.Security.Claims;

namespace ReastosMenu.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly IBookingService _bookingService;

        public AdminController(IBookingService bookingService)
        {
            _bookingService=bookingService;
        }


        public IActionResult Index()
        {
            var bookList = _bookingService.GetAllBooking();

            var bookModelList = bookList.Select(book => new BookingModel
            {
                Name = book.Name,
                Email = book.Email,
                DateAndTime = book.DateAndTime,
                NumberOfPeople = book.NumberOfPeople,
                SpecialRequest = book.SpecialRequest,
            }).ToList();
            return View(bookModelList);
        }


        public IActionResult AccountSetting()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AccountSetting(LoginModel loginModel)
        {
            return View();
        }



    }
}
