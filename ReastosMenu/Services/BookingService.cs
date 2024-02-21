using ReastosMenu.Data;
using ReastosMenu.Entities;
using ReastosMenu.Models;
using System.ComponentModel.DataAnnotations;

namespace ReastosMenu.Services
{
    public interface IBookingService
    {
        List<Booking> GetAllBooking();
        Task<Booking> AddBooking(BookingModel bookingModel);
        Task<Booking> GetBookingById(int id);
        Task DeleteBook(int id);
    }
    public class BookingService : IBookingService
    {
        private readonly RestosMenuDbContext _context;

        public BookingService(RestosMenuDbContext context)
        {
            _context=context;
        }

        public List<Booking> GetAllBooking()
        {
            var bookingList = _context.Bookings
                .OrderBy(b => b.Id)
                .ToList();

            return bookingList;
        }

        public async Task<Booking> GetBookingById(int id)
        {
            return await _context.Bookings.FindAsync(id);

        }

        public async Task<Booking> AddBooking(BookingModel bookingModel)
        {
            try
            {

                var bookingEntity = new Booking
                {
                    Id = bookingModel.Id,
                    Name = bookingModel.Name,
                    Email = bookingModel.Email,
                    DateAndTime = bookingModel.DateAndTime,
                    SpecialRequest = bookingModel.SpecialRequest,
                    NumberOfPeople = bookingModel.NumberOfPeople,
                };

                await _context.Bookings.AddAsync(bookingEntity);
                await _context.SaveChangesAsync();

                return bookingEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteBook(int id)
        {
            var book = await _context.Bookings.FindAsync(id);
            if (book == null)
            {
                throw new ValidationException("Id is not found");
            }

            _context.Bookings.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
