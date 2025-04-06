using Bonded.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bonded.Application.Interfaces;

namespace Bonded.Application.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)  
        {
            _postRepository = postRepository;
        }

        // Get all posts by a specific user
        public async Task<List<Post>> ShowPostsAsync(string userId)
        {
            return await _postRepository.ShowPostsAsync(userId);
        }

        // Get a post by its ID
        public Post GetPostById(int postId)
        {
            return _postRepository.GetPostById(postId);
        }

        // Async version of GetPostById
        public async Task<Post> GetPostByIdAsync(int postId)
        {
            return await _postRepository.GetPostByIdAsync(postId);
        }

        // Add a new post to the database
        public void StorePost(Post newPost)
        {
            _postRepository.StorePost(newPost);
        }

        // Async version of StorePost
        public async Task StorePostAsync(Post newPost)
        {
            await _postRepository.StorePostAsync(newPost);
        }

        // Get the UserId associated with a specific PostId
        public string GetUserIdByPostId(int postId)
        {
            return _postRepository.GetUserIdByPostId(postId);
        }

        // Async version of GetUserIdByPostId
        public async Task<string> GetUserIdByPostIdAsync(int postId)
        {
            return await _postRepository.GetUserIdByPostIdAsync(postId);
        }
        public async Task EditPostAsync(int postId, Post updatedPost)
        {
            await _postRepository.EditPostAsync(postId, updatedPost); 
        }
        public async Task DeletePostAsync(int postId)
        {
            await _postRepository.DeletePostAsync(postId);
        }
        public void EditPost(int postId, Post updatedPost)
        {
          _postRepository.EditPost(postId, updatedPost);
        }
        public List<CarousalImage> CarouselImages()
        {
            return _postRepository.CarouselImages();
        }
    }
}






//using Bonded.Models.Interfaces;
//using Microsoft.AspNetCore.Mvc.ActionConstraints;
//using Microsoft.Data.SqlClient;
//namespace Bonded.Models
//{
//    public class PostRepository : IPostRepository
//    {
//        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PassionConnect;Integrated Security=True";
//        public List<Post> showPosts(int userId)
//        {

//            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
//            {
//                UserRepository uR = new UserRepository();
//                List<Post> posts = new List<Post>();
//                sqlConnection.Open();
//                string query = "SELECT * FROM [Posts] WHERE UserId = @Id";
//                SqlCommand cmd = new SqlCommand(query, sqlConnection);
//                cmd.Parameters.AddWithValue("@Id", userId);

//                SqlDataReader reader = cmd.ExecuteReader();
//                while (reader.Read())
//                {
//                    Post post = new Post
//                    {
//                        Id = reader.GetInt32(0),
//                        UserId = reader.GetInt32(1),
//                        Poster = uR.GetUserById(userId),
//                        Content = reader.GetString(2),
//                        ImagePath = reader.GetString(3),
//                        CreatedAt = reader.GetDateTime(4),
//                        IsPrivate = reader.GetBoolean(5)
//                    };
//                    posts.Add(post);

//                }
//                return posts;
//            }

//        }
//        public Post GetPostById(int postId)
//        {
//            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
//            {

//                UserRepository uR = new UserRepository();

//                sqlConnection.Open();

//                string query = "SELECT * FROM [Posts] WHERE Id = @Id";
//                SqlCommand cmd = new SqlCommand(query, sqlConnection);
//                cmd.Parameters.AddWithValue("@Id", postId);

//                SqlDataReader reader = cmd.ExecuteReader();

//                if (reader.Read())
//                {

//                    Post post = new Post
//                    {
//                        Id = reader.GetInt32(0),
//                        UserId = reader.GetInt32(1),
//                        Poster = uR.GetUserById(reader.GetInt32(1)),
//                        Content = reader.GetString(2),
//                        ImagePath = reader.GetString(3),
//                        CreatedAt = reader.GetDateTime(4),
//                        IsPrivate = reader.GetBoolean(5)
//                    };

//                    return post;
//                }
//            }

//            return null;
//        }
//        public async Task<Post> GetPostByIdAsync(int postId)
//        {
//            try
//            {
//                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
//                {
//                    UserRepository uR = new UserRepository();

//                    await sqlConnection.OpenAsync();  // Asynchronously open the connection

//                    string query = "SELECT * FROM [Posts] WHERE Id = @Id";
//                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
//                    cmd.Parameters.AddWithValue("@Id", postId);

//                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())  // Asynchronously execute the query
//                    {
//                        if (await reader.ReadAsync())  // Asynchronously read the data
//                        {
//                            Post post = new Post
//                            {
//                                Id = reader.GetInt32(0),
//                                UserId = reader.GetInt32(1),
//                                Poster = await uR.GetUserByIdAsync(reader.GetInt32(1)),  // Assuming GetUserByIdAsync is asynchronous
//                                Content = reader.GetString(2),
//                                ImagePath = reader.GetString(3),
//                                CreatedAt = reader.GetDateTime(4),
//                                IsPrivate = reader.GetBoolean(5)
//                            };

//                            return post;  // Return the post object if found
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Log the exception(you can use a logging framework here)
//                Console.WriteLine($"Error fetching post: {ex.Message}");
//            }

//            return null;  // Return null if no post is found or an error occurred
//        }

//        public void StorePost(Post newPost)
//        {
//            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
//            {
//                sqlConnection.Open();

//                string query = "INSERT INTO [Posts] (UserId, Content, ImagePath, CreatedAt, IsPrivate) " +
//                               "VALUES (@UserId, @Content, @ImagePath, @CreatedAt, @IsPrivate)";

//                SqlCommand cmd = new SqlCommand(query, sqlConnection);
//                cmd.Parameters.AddWithValue("@UserId", newPost.UserId);
//                cmd.Parameters.AddWithValue("@Content", newPost.Content);
//                cmd.Parameters.AddWithValue("@ImagePath", newPost.ImagePath);
//                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
//                cmd.Parameters.AddWithValue("@IsPrivate", newPost.IsPrivate);

//                cmd.ExecuteNonQuery();
//            }
//        }
//        public int GetUserIdByPostId(int postId)
//        {
//            using (var connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                var query = "SELECT UserId FROM Posts WHERE Id = @PostId";
//                using (var command = new SqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@PostId", postId);
//                    var result = command.ExecuteScalar();

//                    if (result == null)
//                    {
//                        throw new Exception("Post not found");
//                    }

//                    return Convert.ToInt32(result);
//                }
//            }
//        }


//    }
//}
