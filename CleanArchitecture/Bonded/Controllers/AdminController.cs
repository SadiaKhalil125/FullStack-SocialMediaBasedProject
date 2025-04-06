using Microsoft.AspNetCore.Mvc;
using Bonded.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Bonded.Infrastructure;
using Bonded.Domain;
using Application.Interfaces;
using Application.Services;

namespace Bonded.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;
       
        public AdminController(AdminService adminservice)
        {
    
            _adminService = adminservice;
        }

        //// Analytics Action
        [Authorize(Policy = "AdminAccess")]
        public IActionResult Dashboard()
        {

            List<int> counts = _adminService.GetCount();
            var analytics = new
            {
                UsersCount = counts[0],
                PostsCount = counts[1],
                LikesCount = counts[2],
                CommentsCount = counts[3],
                FollowsCount = counts[4],
                MessagesCount = counts[5]
            };

            return View(analytics);
        }
        [HttpGet]
        public IActionResult GetChartData()
        {
            List<int> counts =  _adminService.GetCount();
            var updatedData = new
            {
                UsersCount = counts[0],
                PostsCount = counts[1],
                LikesCount = counts[2],
                CommentsCount = counts[3],
                FollowsCount = counts[4],
                MessagesCount = counts[5]
            };

            return Json(updatedData);
        }
        // Manage Carousel Images Action
        [Authorize(Policy = "AdminAccess")]
        public IActionResult ManageCarousel()
        {
            //var images = new List<CarousalImage>() { new CarousalImage { ImageUrl = "/wwwroot/images/carousel/4d8f5fbf-6367-468d-8c29-b8b2a2a452df.jpg"},
            //new CarousalImage { ImageUrl = "/images/carousel/e6c9b20f-30e5-4e2f-a593-0b7ba66c11ab.jpg"},new CarousalImage { ImageUrl = "/wwwroot/images/carousel/a92d6fcd-d5fb-4366-861c-5b1ca7cac1cb.jpg" } };
            var images = _adminService.GetCarousalImages();
            return View(images);
        }


        [HttpPost]
        public IActionResult ManageCarousel(List<IFormFile> files)
        {
            if (files.Count > 0)
            {
                // Remove all previous carousel images (or modify this logic if needed)
                var carouselImages = _adminService.GetCarousalImages();
                _adminService.RemoveCarousals(carouselImages);

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        try
                        {
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "carousel", fileName);

                            var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "carousel");
                            if (!Directory.Exists(imagesDirectory))
                            {
                                Directory.CreateDirectory(imagesDirectory);
                            }

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                            _adminService.addCarousalImage(fileName);
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", "Error uploading the file: " + ex.Message);
                            return View();
                        }
                    }
                }

            
            }

            return RedirectToAction("ManageCarousel");
        }




     
    }


}
