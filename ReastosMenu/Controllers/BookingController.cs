using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReastosMenu.Models;
using ReastosMenu.Services;

namespace ReastosMenu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BookingController : Controller
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingService _bookingService;

        public BookingController(ILogger<BookingController> logger, IBookingService bookingService)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var bookingList = _bookingService.GetAllBooking();

            var bookModel = bookingList.Select(b => new BookingModel
            {
                Name = b.Name,
                Email = b.Email,
                DateAndTime = b.DateAndTime,
                NumberOfPeople = b.NumberOfPeople,
                SpecialRequest = b.SpecialRequest,
            }).ToList();
            return View(bookModel);
        }

        [HttpGet]
        public IActionResult AddBooking()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBooking(BookingModel bookingModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookingModel);
            }

            await _bookingService.AddBooking(bookingModel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookingService.DeleteBook(id);
            return RedirectToAction("Index", "Booking");
        }
    }
}
