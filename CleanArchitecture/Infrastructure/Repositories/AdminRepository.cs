using Application.Interfaces;
using Bonded.Domain;
using Bonded.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AdminRepository :IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<int> GetAnalytics()
        {
            var analytics = new List<int>()
            {
                _context.Users.Count(),
                _context.Posts.Count(),
               _context.Likes.Count(),
                _context.Comments.Count(),
                _context.Follows.Count(),
               _context.Convos.Count()
            };
            return analytics;
        }
        public List<CarousalImage> GetCarousalImages()
        {

            var images = _context.CarousalImages.Take(3).ToList();
            return images;
        }
        public void addCarousalImage(string fileName)
        {
            _context.CarousalImages.Add(new CarousalImage { ImageUrl = "/images/carousel/" + fileName });
            _context.SaveChanges();
        }
        public void RemoveCarousals(List<CarousalImage> images)
        {
            _context.CarousalImages.RemoveRange(images); 
        }
    }
}
