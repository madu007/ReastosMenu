using System.ComponentModel.DataAnnotations;

namespace ReastosMenu.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
