using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ReastosMenu.Models
{
    public class MenuModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
       
        [Required(ErrorMessage = "Please select a category")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
        public int SelectedCategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        //public List<string> ImagePaths { get; set; }
        [Required]
        public string ImagesUrl { get; set; }
    }
}
