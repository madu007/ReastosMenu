using ReastosMenu.Data;
using ReastosMenu.Entities;

namespace ReastosMenu.Services
{
    public interface IBannerService
    {
        List<Banner> GetALLBanner();
    }
    public class BannerService : IBannerService
    {
        private readonly RestosMenuDbContext _context;

        public BannerService(RestosMenuDbContext context)
        {
            _context = context;
        }

        public List<Banner> GetALLBanner()
        {
            var bannerList = _context.Banner
                .OrderBy(b => b.Id)
                .ToList();

            bannerList = bannerList.Select(b =>
            {
                b.BannerImages = _context.BannerImages.Where(pi => pi.BannerId == b.Id).ToList();
                return b;
            }).ToList();

            return bannerList;
        }
    }
}
