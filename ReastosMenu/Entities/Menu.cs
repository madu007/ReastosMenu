namespace ReastosMenu.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagesUrl { get; set; }
        public virtual Category Category { get; set; }
    }
}
