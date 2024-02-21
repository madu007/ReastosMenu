using System.ComponentModel.DataAnnotations;

namespace ReastosMenu.Entities
{
    public class MenuImage
    {
        public int Id { get; set; }

        [Required]
        public int MenuId { get; set; }
        [Required]
        [StringLength(50)]
        public string Images { get; set; }
    }
}
