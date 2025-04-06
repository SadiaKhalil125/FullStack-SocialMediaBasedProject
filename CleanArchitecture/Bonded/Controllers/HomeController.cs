using Bonded.Application.Interfaces;
using Bonded.Application.Services;
using Bonded.Domain;
using Bonded.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Bonded.Controllers
{
    public class HomeController : Controller
    {

        private readonly UserManager<User> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly PostService _postService;
    
    

        public HomeController(UserManager<User> userManager,PostService postService)
        {
            _userManager = userManager;
          

            _postService = postService;

        }
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Search(string searchTerm)
        {

            if (string.IsNullOrEmpty(searchTerm))
            {
                return View(new List<User>());
            }
            searchTerm = searchTerm.ToLower();
            var users = await _userManager.Users
            .Where(u => u.UserName.ToLower().Contains(searchTerm) ||
                   u.Email.ToLower().Contains(searchTerm))
              .ToListAsync();
           // var users = await _userManager.FindByNameAsync(searchTerm);
            return View(users);
        }
        public IActionResult Index()
        {
          //  SeedData.SeedRolesAsync(_roleManager);
            string? userId = HttpContext.Session.GetString("UserId");
            string userIdValue = userId ?? "";

            var images  = _postService.CarouselImages();
            //if (userIdValue == "")
            //{
            //    TempData["ErrorMessage"] = "Signin to view more!";
            //    return RedirectToAction("Login", "User");
            //}
            TempData["Id"] = userIdValue;
            return View(images);
        }
        public IActionResult GlobalConnections()
        {
            return View();
        }

        public IActionResult ShareIdeas()
        {
            return View();
        }

        public IActionResult GrowTogether()
        {
            return View();
        }  
        public IActionResult Privacy()
        {
            return View();
        }


    }
}