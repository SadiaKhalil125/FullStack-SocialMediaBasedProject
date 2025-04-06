using Bonded.Domain;
using System.Collections.Generic;
using Bonded.Application.Interfaces;

namespace Bonded.Application.Services
{
    public class CommentService
    {
      
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        // Add a new comment to the database asynchronously
        public async Task<bool> AddCommentAsync(Comment comment)
        {
            return await _commentRepository.AddCommentAsync(comment);
        }

        // Retrieve all comments for a specific post asynchronously
        public async Task<List<Comment>> ViewCommentsAsync(int postId)
        {
            return await _commentRepository.ViewCommentsAsync(postId);
        }

        // Retrieve comments with user details asynchronously
        public async Task<List<Comment>> ViewCommentsWithUserDetailsAsync(int postId)
        {
            return await _commentRepository.ViewCommentsWithUserDetailsAsync(postId);
            //return await _commentRepository.ViewCommentsWithUserDetailsAsync(postId);
        }
        public async Task DeleteComment(int CommentId)
        {
           await _commentRepository.DeleteComment(CommentId);
        }
        public void DeleteCommentsOfAPost(int postId)
        {
           _commentRepository.DeleteCommentsOfAPost(postId);
        }

    }
}

















//using Bonded.Models.Interfaces;
//using Microsoft.Data.SqlClient;
//using Bonded.Models.ViewModels;
//namespace Bonded.Models
//{
//    public class CommentRepository:ICommentRepository
//    {
//        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PassionConnect;Integrated Security=True";

//        // Get all notifications for a specific user
//        public bool AddComment(Comment comment)
//        {
//            int r = 0;
//            using (var connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                var query = "INSERT INTO Comments (UserId,Content,PostId) VALUES (@userid,@content,@postid)";
//                using (var command = new SqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@userid", comment.UserId);
//                    command.Parameters.AddWithValue("@content", comment.Content);
//                    command.Parameters.AddWithValue("@postid", comment.PostId);
//                    r = command.ExecuteNonQuery();
//                }
//            }
//            return r>0;
//        }
//        public List<CommentsDetailViewModel> ViewComments(int postId)
//        {
//            UserRepository userRepository = new UserRepository();   
//            List<CommentsDetailViewModel> comments = new List<CommentsDetailViewModel>(); 
//            using (var connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                var query = "SELECT * FROM Comments WHERE PostId = @PostId";
//                using (var command = new SqlCommand(query, connection))
//                {

//                    command.Parameters.AddWithValue("@Postid", postId);
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        CommentsDetailViewModel comment = new CommentsDetailViewModel();
//                        Comment comm = new Comment(){ Id = reader.GetInt32(0),UserId = reader.GetInt32(1),Content=reader.GetString(2),PostId=reader.GetInt32(3) };
//                        User user = userRepository.GetUserById(reader.GetInt32(1));
//                        comment.Comment = comm;
//                        comment.User = user;
//                        comments.Add(comment);
//                    }

//                }
//            }


//            return comments;
//        }

//    }
//}

