using Bonded.Application.Interfaces;
using Bonded.Application.Services;
using Bonded.Domain;
using Bonded.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class FollowController : Controller
{
    private readonly FollowService _followService;
    private readonly UserManager<User> _userManager;
    private readonly NotificationService _notificationService;
    public FollowController(FollowService followService, UserManager<User> userManager, NotificationService notificationService)
    {
        _followService = followService;
        _userManager = userManager;
        _notificationService = notificationService;
    }
    [HttpGet]
    public async Task<IActionResult> ShowFollowingAsync(string userId)
    {
        // Get the list of users that the specified user is following
        var followingIds = await _followService.GetFollowingIdsAsync(userId);
        List<User> followings = new List<User>();
        foreach (string id in followingIds)
        {
            followings.Add(await _userManager.FindByIdAsync(id));
        }

        return View(followings);// Return the list to the view
    }
    public async Task<IActionResult> ShowFollowerAsync(string userId)
    {
        // Fetch the followers' IDs
        List<string> followerIds = await _followService.GetFollowersIdsAsync(userId);

        // Fetch the user details for each follower ID
        List<User> followers = new List<User>();
        foreach (var followerId in followerIds)
        {
            var user = await _userManager.FindByIdAsync(followerId); // Adjust this call to get user by ID
            if (user != null)
            {
                followers.Add(user);
            }
        }

        // Pass the list of followers to the view
        return View(followers);
    }
    [HttpPost]
    public async Task<IActionResult> ToggleFollow(string followerId, string followingId)
    {
        if (string.IsNullOrEmpty(followerId) || string.IsNullOrEmpty(followingId))
        {
            return Json(new { success = false, message = "Invalid data" });
        }

        bool isFollowing = await _followService.IsFollowingAsync(followerId, followingId);

        if (isFollowing)
        {
            await _followService.UnfollowUserAsync(followerId, followingId);
            isFollowing = false;
        }
        else
        {
            await _followService.FollowUserAsync(followerId, followingId);
            isFollowing = true;
        }

        int followerCount =  _followService.GetFollowersCountAsync(followingId);

        return Json(new { success = true, isFollowing, followerCount });
    }

    //[HttpPost]
    //public async Task<IActionResult> ToggleFollow(string followerId, string followingId)
    //{
    //    if (followerId == followingId)
    //    {
    //        TempData["ErrorMessage"] = "Can't follow yourself!";

    //        return RedirectToAction("ViewOtherProfiles", "User", new { id = followingId });
    //        //return BadRequest("You cannot follow yourself.");
    //    }

    //    var isFollowing = await _followService.IsFollowingAsync(followerId, followingId);

    //    if (isFollowing)
    //    {

    //        await _followService.UnfollowUserAsync(followerId, followingId);
    //        await _notificationService.DeleteNotificationForUnfollowAsync(followerId, 1);

    //    }
    //    else
    //    {
    //        await _followService.FollowUserAsync(followerId, followingId);
    //        var user = await _userManager.FindByIdAsync(followerId);
    //        await _notificationService.AddNotificationAsync(followingId, $"{user.UserName} has followed you!", 1);
    //    }

    //    return RedirectToAction("ViewOtherProfiles", "User", new { id = followingId });
    //}

    public JsonResult GetFollowerFollowingCounts()
    {
        string? userId = HttpContext.Session.GetString("UserId");
        string userIdValue = userId ?? "";

        var followers = _followService.GetFollowersCountAsync(userIdValue); //_context.Followers.Count(f => f.FollowedUserId == UserId);
        var following = _followService.GetFollowingCountAsync(userIdValue); 

        return Json(new { followers, following });
    }



}

