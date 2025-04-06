using Bonded.Application.Interfaces;
using Bonded.Application.Services;
using Bonded.Domain;
using Bonded.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


public class LikeController : Controller
{
    private readonly LikeService _likeService;
    private readonly NotificationService _notificationService;
    private readonly PostService _postService;
    private readonly UserManager<User> _userManager;

    public LikeController(LikeService likeService, NotificationService notificationService, PostService postService, UserManager<User> userManager)
    {
        _likeService = likeService;
        _notificationService = notificationService;
        _postService = postService;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> ToggleLike(int postId)
    {
        try
        {
           
            string? userId = HttpContext.Session.GetString("UserId");
            string UserIdValue = userId ?? "";
            if (string.IsNullOrEmpty(UserIdValue))
            {
                return Json(new { success = false, message = "You must be logged in to like a post." });
            }
            Console.WriteLine("here error1");
            bool isLiked = _likeService.IsPostLikedByUser(UserIdValue, postId);
            Console.WriteLine("here error2");
            if (isLiked)
            {
                Console.WriteLine("here error3");
                _likeService.UnlikePost(postId, UserIdValue);
                await _notificationService.DeleteNotificationForUnlikePostAsync(UserIdValue, postId);
            }
            else
            {
                Console.WriteLine("here error4");
                string postOwnerId = _postService.GetUserIdByPostId(postId);
                var user =  await _userManager.FindByIdAsync(UserIdValue);
                await _notificationService.AddNotificationAsync(postOwnerId, $"{user.UserName} has liked your post!", postId);
              
                _likeService.LikePost(postId, UserIdValue);
            }

            int likeCount = _likeService.GetLikeCount(postId);
            bool isliked = !isLiked;
            return Json(new { success = true, isliked, likeCount });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in ToggleLikeAsync: ", ex.Message);
            return Json(new { success = false, message = "An unexpected error occurred." });
        }
    }

}

