using Bonded.Domain;
using Bonded.Application.Interfaces;
namespace Bonded.Application.Services
{
    public class LikeService
    {
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        // Check if a user has liked a specific post
        public async Task<bool> IsPostLikedByUserAsync(string userId, int postId)
        {
            return await _likeRepository.IsPostLikedByUserAsync(userId, postId);
        }
        public bool IsPostLikedByUser(string userId, int postId)
        {
            return _likeRepository.IsPostLikedByUser(userId, postId);
        }

        // Get the count of likes for a post
        public async Task<int> GetLikeCountAsync(int postId)
        {
            return await _likeRepository.GetLikeCountAsync(postId);
        }
        public int GetLikeCount(int postId)
        {
            return _likeRepository.GetLikeCount(postId);
        }

        public void LikePost(int postId, string userId)
        {
            _likeRepository.LikePost(postId, userId);
        }

        // Remove a like from a post
        public void UnlikePost(int postId, string userId)
        {
            _likeRepository.UnlikePost(postId, userId);
        }

        // Check if a user has liked a specific post (sync version)
        public bool IsPostLikedByUser(int postId, string userId)
        {
            return _likeRepository.IsPostLikedByUser(postId,userId);
        }

        // Get the list of post IDs liked by the user
        public async Task<List<int>> GetLikedPostIdsAsync(string userId)
        {
            return await _likeRepository.GetLikedPostIdsAsync(userId);
        }
        public void DeleteLikesOfAPost(int postId)
        {
          _likeRepository.DeleteLikesOfAPost(postId);
        }
    }
}




























//using Bonded.Models;
//using System.Collections.Generic;
//using Microsoft.Data.SqlClient;
//using System.Threading.Tasks;


//public class LikeRepository : ILikeRepository
//{
//    private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PassionConnect;Integrated Security=True";
//    public async Task<bool> IsPostLikedByUserAsync(int userId, int postId)
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            await connection.OpenAsync();

//            var query = "SELECT COUNT(*) FROM Likes WHERE UserId = @UserId AND PostId = @PostId";
//            using (var command = new SqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@UserId", userId);
//                command.Parameters.AddWithValue("@PostId", postId);

//                var count = (int)await command.ExecuteScalarAsync();
//                return count > 0; // Return true if the count is greater than 0
//            }
//        }
//    }

//    public async Task<int> GetLikeCountAsync(int postId)
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            await connection.OpenAsync();

//            var query = "SELECT COUNT(*) FROM Likes WHERE PostId = @PostId";
//            using (var command = new SqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@PostId", postId);

//                return (int)await command.ExecuteScalarAsync(); // Return the count directly
//            }
//        }
//    }
//    public bool IsPostLikedByUser(int postId, int userId)
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            connection.Open();
//            string query = "SELECT COUNT(*) FROM Likes WHERE PostId = @PostId AND UserId = @UserId";
//            SqlCommand cmd = new SqlCommand(query, connection);
//            cmd.Parameters.AddWithValue("@PostId", postId);
//            cmd.Parameters.AddWithValue("@UserId", userId);
//            int likeCount = (int)cmd.ExecuteScalar();
//            return likeCount > 0;  // If there's a like, return true, otherwise false
//        }
//    }

//    public void LikePost(int postId, int userId)
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            connection.Open();
//            string query = "INSERT INTO Likes (PostId, UserId, CreatedAt) VALUES (@PostId, @UserId, GETDATE())";
//            SqlCommand cmd = new SqlCommand(query, connection);
//            cmd.Parameters.AddWithValue("@PostId", postId);
//            cmd.Parameters.AddWithValue("@UserId", userId);
//            cmd.ExecuteNonQuery();  // Insert the like into the database
//        }
//    }

//    public void UnlikePost(int postId, int userId)
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            connection.Open();
//            string query = "DELETE FROM Likes WHERE PostId = @PostId AND UserId = @UserId";
//            SqlCommand cmd = new SqlCommand(query, connection);
//            cmd.Parameters.AddWithValue("@PostId", postId);
//            cmd.Parameters.AddWithValue("@UserId", userId);
//            cmd.ExecuteNonQuery();  // Delete the like from the database
//        }
//    }

//}

