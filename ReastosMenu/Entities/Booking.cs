using ReastosMenu.Models;
using System.ComponentModel.DataAnnotations;

namespace ReastosMenu.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateAndTime { get; set; } = DateTime.Now;

        public NumberOfPerson NumberOfPeople { get; set; }

        public string SpecialRequest { get; set; }
    }

   
}
