namespace ReastosMenu.Models
{
    public class LandingPageModel
    {
        public List<MenuModel> BreakFastMenu { get; set; }
        public List<MenuModel> LuchMenu { get; set; }
        public List<MenuModel> DinnerMenu { get; set; }
        public List<BannerModel> Banners { get; set; }
        public BookingModel Booking { get; set; }
    }
}
