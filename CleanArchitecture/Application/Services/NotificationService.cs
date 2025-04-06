using Bonded.Application.Interfaces;
using Bonded.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bonded.Application.Services
{
    public class NotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        // Get all notifications for a specific user
        public async Task<List<Notification>> GetAllNotificationsAsync(string userId)
        {
            return await _notificationRepository.GetAllNotificationsAsync(userId);
        }

        // Get notifications for the current day for a specific user
        public async Task<List<Notification>> GetTodayNotificationsAsync(string userId)
        {
            return await _notificationRepository.GetTodayNotificationsAsync(userId);
        }

        // Add a new notification
        public async Task AddNotificationAsync(string userId, string message, int relatedId)
        {
            await _notificationRepository.AddNotificationAsync(userId,message,relatedId);
        }
        public async Task DeleteNotificationForUnfollowAsync(string userId, int unfollowedUserId)
        {
            await _notificationRepository.DeleteNotificationForUnfollowAsync(userId, unfollowedUserId);
        }
        public async Task DeleteNotificationForUnlikePostAsync(string userId, int postId)
        {
           await _notificationRepository.DeleteNotificationForUnlikePostAsync(userId, postId);
        }

        public void DeleteNotification(int id)
        {
            _notificationRepository.DeleteNotification(id);
        }
        public async Task<List<Notification>> GetLatest10Notifications(string userId)
        {
           return await _notificationRepository.GetLatest10Notifications(userId);
        }
    }
}
























//using System;
//using System.Collections.Generic;
//using Microsoft.Data.SqlClient;
//using System.Linq;
//using Bonded.Models.Interfaces;

//namespace Bonded.Models
//{
//    public class NotificationRepository:INotificationRepository
//    {
//        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PassionConnect;Integrated Security=True";

//        // Get all notifications for a specific user
//        public List<Notification> GetAllNotifications(int userId)
//        {
//            var notifications = new List<Notification>();
//            using (var connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                var query = "SELECT * FROM Notifications WHERE UserId = @UserId ORDER BY CreatedAt DESC";
//                using (var command = new SqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@UserId", userId);
//                    using (var reader = command.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            notifications.Add(new Notification
//                            {
//                                Id = reader.GetInt32(0),
//                                UserId = reader.GetInt32(1),
//                                Message = reader.GetString(2),
//                                CreatedAt = reader.GetDateTime(3),
//                                RelatedId = reader.GetInt32(4)
//                            });
//                        }
//                    }
//                }
//            }
//            return notifications;
//        }

//        // Get notifications for the current day for a specific user
//        public List<Notification> GetTodayNotifications(int userId)
//        {
//            var notifications = new List<Notification>();
//            using (var connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                var query = @"
//                    SELECT * FROM Notifications 
//                    WHERE UserId = @UserId AND CAST(CreatedAt AS DATE) = CAST(GETDATE() AS DATE)
//                    ORDER BY CreatedAt DESC";
//                using (var command = new SqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@UserId", userId);
//                    using (var reader = command.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            notifications.Add(new Notification
//                            {
//                                Id = reader.GetInt32(0),
//                                UserId = reader.GetInt32(1),
//                                Message = reader.GetString(2),
//                                CreatedAt = reader.GetDateTime(3),
//                                RelatedId = reader.GetInt32(4)
//                            });
//                        }
//                    }
//                }
//            }
//            return notifications;
//        }

//        // Add a new notification
//        public void AddNotification(int userId, string message , int relatedId)
//        {

//            using (var connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                var query = "INSERT INTO Notifications (UserId, Message,RelatedId) VALUES (@UserId, @Message,@Relatedid)";
//                using (var command = new SqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@UserId", userId);
//                    command.Parameters.AddWithValue("@Message", message);
//                    command.Parameters.AddWithValue("@Relatedid", relatedId);
//                    command.ExecuteNonQuery();
//                }
//            }
//        }
//    }
//}
