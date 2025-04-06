using Bonded.Domain;
using Bonded.Application.Interfaces;

namespace Bonded.Application.Services
{
    public class FollowService
    {
        private readonly IFollowRepository _followRepository;

        public FollowService(IFollowRepository followRepository)
        {
            _followRepository = followRepository;         
        }

        // Follow a user
        public async Task<bool> FollowUserAsync(string followerId, string followingId)
        {
            return await _followRepository.FollowUserAsync(followerId, followingId);
        }

        // Unfollow a user
        public async Task<bool> UnfollowUserAsync(string followerId, string followingId)
        {
           return await _followRepository.UnfollowUserAsync(followerId, followingId);
        }

        // Toggle follow/unfollow
        public async Task<bool> ToggleFollowAsync(string followerId, string followingId)
        {
           return await _followRepository.ToggleFollowAsync(followerId, followingId);
        }

        // Check if the user is following another user
        public async Task<bool> IsFollowingAsync(string followerId, string followingId)
        {
           return await _followRepository.IsFollowingAsync(followerId, followingId);
        }

        // Get list of following users' ids
        public async Task<List<string>> GetFollowingIdsAsync(string userId)
        {
           return await _followRepository.GetFollowingIdsAsync(userId);
        }

        // Get list of follower users' ids
        public async Task<List<string>> GetFollowersIdsAsync(string userId)
        {
            return await _followRepository.GetFollowersIdsAsync(userId);
        }

        // Get number of followers
        public int GetFollowersCountAsync(string userId)
        {
            return  _followRepository.GetFollowersCountAsync(userId);
        }

        // Get number of following
        public int GetFollowingCountAsync(string userId)
        {
            return  _followRepository.GetFollowingCountAsync(userId);
        }
    }
}




//using Bonded.Models;
//using System.Collections.Generic;
//using Microsoft.Data.SqlClient;
//using System.Threading.Tasks;


//public class FollowRepository : IFollowRepository
//{
//    private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PassionConnect;Integrated Security=True";

//    public async Task<bool> FollowUser(int followerId, int followingId)
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            await connection.OpenAsync();
//            var query = "IF NOT EXISTS (SELECT 1 FROM Follows WHERE FollowerId = @FollowerId AND FollowingId = @FollowingId) " +
//                        "INSERT INTO Follows (FollowerId, FollowingId) VALUES (@FollowerId, @FollowingId)";
//            var command = new SqlCommand(query, connection);
//            command.Parameters.AddWithValue("@FollowerId", followerId);
//            command.Parameters.AddWithValue("@FollowingId", followingId);

//            var result = await command.ExecuteNonQueryAsync();
//            return result > 0;
//        }
//    }

//    public async Task<bool> UnfollowUser(int followerId, int followingId)
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            await connection.OpenAsync();
//            var query = "DELETE FROM Follows WHERE FollowerId = @FollowerId AND FollowingId = @FollowingId";
//            var command = new SqlCommand(query, connection);
//            command.Parameters.AddWithValue("@FollowerId", followerId);
//            command.Parameters.AddWithValue("@FollowingId", followingId);

//            var result = await command.ExecuteNonQueryAsync();
//            return result > 0;
//        }
//    }
//    public async Task<bool> ToggleFollow(int followerId, int followingId)
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            await connection.OpenAsync();

//            // Check if already following
//            var isFollowingQuery = "SELECT COUNT(*) FROM Follows WHERE FollowerId = @FollowerId AND FollowingId = @FollowingId";
//            var isFollowingCommand = new SqlCommand(isFollowingQuery, connection);
//            isFollowingCommand.Parameters.AddWithValue("@FollowerId", followerId);
//            isFollowingCommand.Parameters.AddWithValue("@FollowingId", followingId);
//            var isFollowing = (int)await isFollowingCommand.ExecuteScalarAsync() > 0;

//            string query;

//            if (isFollowing)
//            {
//                // Unfollow logic
//                query = "DELETE FROM Follows WHERE FollowerId = @FollowerId AND FollowingId = @FollowingId";
//            }
//            else
//            {
//                // Follow logic
//                query = "INSERT INTO Follows (FollowerId, FollowingId) VALUES (@FollowerId, @FollowingId)";
//            }

//            var command = new SqlCommand(query, connection);
//            command.Parameters.AddWithValue("@FollowerId", followerId);
//            command.Parameters.AddWithValue("@FollowingId", followingId);

//            var result = await command.ExecuteNonQueryAsync();
//            return result > 0;
//        }
//    }

//    public async Task<bool> IsFollowingAsync(int followerId, int followingId)
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            await connection.OpenAsync();
//            var query = "SELECT COUNT(*) FROM Follows WHERE FollowerId = @FollowerId AND FollowingId = @FollowingId";
//            var command = new SqlCommand(query, connection);
//            command.Parameters.AddWithValue("@FollowerId", followerId);
//            command.Parameters.AddWithValue("@FollowingId", followingId);

//            var result = (int)await command.ExecuteScalarAsync();
//            return result > 0;
//        }
//    }







//    public List<int> GetFollowingIds(int userId)
//    {
//        List<int> followingIds = new List<int>();

//        using (SqlConnection connection = new SqlConnection(_connectionString))
//        {
//            connection.Open();

//            string query = "SELECT FollowingId FROM Follows WHERE FollowerId = @UserId";
//            using (SqlCommand command = new SqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@UserId", userId);
//                SqlDataReader reader = command.ExecuteReader();

//                while (reader.Read())
//                {
//                    followingIds.Add(reader.GetInt32(0)); // Add FollowingId to list
//                }
//            }
//        }

//        return followingIds;
//    }
//    public List<int> GetFollowersIds(int userId)
//    {
//        List<int> followerIds = new List<int>();

//        using (SqlConnection connection = new SqlConnection(_connectionString))
//        {
//            connection.Open();

//            string query = "SELECT FollowerId FROM Follows WHERE FollowingId = @UserId";
//            using (SqlCommand command = new SqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@UserId", userId);
//                SqlDataReader reader = command.ExecuteReader();

//                while (reader.Read())
//                {
//                    followerIds.Add(reader.GetInt32(0)); // Add FollowerId to list
//                }
//            }
//        }

//        return followerIds;
//    }
//    public int GetFollowersCount(int userId)
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            connection.Open();
//            var query = "SELECT COUNT(*) FROM Follows WHERE FollowingId = @UserId";
//            using (var command = new SqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@UserId", userId);
//                return (int)command.ExecuteScalar();
//            }
//        }
//    }

//    public int GetFollowingCount(int userId)
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            connection.Open();
//            var query = "SELECT COUNT(*) FROM Follows WHERE FollowerId = @UserId";
//            using (var command = new SqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@UserId", userId);
//                return (int)command.ExecuteScalar();
//            }
//        }
//    }

//}
