using System.ComponentModel.DataAnnotations;

namespace ReastosMenu.Models
{
    public class BookingModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime DateAndTime { get; set; } = DateTime.Now;

        [Required]
        public NumberOfPerson NumberOfPeople { get; set; }

        public string SpecialRequest { get; set; }
    }
}
