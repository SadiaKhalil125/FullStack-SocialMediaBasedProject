﻿using Bonded.Application.Interfaces;
using Bonded.Application.Services;
using Bonded.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bonded.Controllers
{
    public class NotificationController : Controller
    {
        private readonly NotificationService _service;
        public NotificationController(NotificationService service)
        {
            _service = service;
        }

        // Show all notifications for a user
        [HttpGet]
        public async Task<IActionResult> AllNotificationsAsync()
        {
            string? userId = HttpContext.Session.GetString("UserId");
            string userIdValue = userId ?? "";
            if (userIdValue == "")
            {
                TempData["ErrorMessage"] = "Signin to view more!";
                return RedirectToAction("Login", "User");
            }

            var notifications = await _service.GetAllNotificationsAsync(userIdValue);
            return View(notifications);
        }

        [HttpGet]
        public async Task<IActionResult> TodayAsync()
        {
            string? userId = HttpContext.Session.GetString("UserId");
            string userIdValue = userId ?? "";
            if (userId == "")
            {
                TempData["ErrorMessage"] = "Signin to view more!";
                return RedirectToAction("Login", "User");
            }

            var notifications = await _service.GetTodayNotificationsAsync(userIdValue);
            return View(notifications);
        }
        [HttpPost]
        public IActionResult DeleteNotification(int id)
        {
            try
            {
                _service.DeleteNotification(id); // Delete the notification
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        //[HttpGet]
        //public IActionResult DeleteNotification(int id)
        //{
        //    _service.DeleteNotification(id);
        //    return RedirectToAction("AllNotifications", "Notification");
        //}




    }
}
