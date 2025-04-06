using Microsoft.AspNetCore.Mvc;
using Bonded.Models.ViewModels;
using Bonded.Domain;
using Microsoft.AspNetCore.Identity;
using Bonded.Application.Interfaces;
using Bonded.Application.Services;
namespace Bonded.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;
        private readonly PostService _postService;
        private readonly NotificationService _notificationService;
        private readonly UserManager<User> _userManager;
        public CommentController(CommentService commentService, PostService postService, NotificationService notificationService, UserManager<User> userManager)
        {
            _commentService = commentService;
            _postService = postService;
            _notificationService = notificationService;
            _userManager = userManager;
        }
        public ActionResult AddComment(int postId)
        {
            // Check if the post exists (optional, based on your logic)


            // Pass the PostId to the view for use in the form
            ViewBag.PostId = postId; // or use ViewData["PostId"]
   
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCommentAsync(int postId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return BadRequest(new { message = "Comment content cannot be empty." });
            }

            string? userId = HttpContext.Session.GetString("UserId");
            string userIdValue = userId ?? "";

            if (string.IsNullOrEmpty(userIdValue))
            {
                return Unauthorized(new { message = "You must log in to comment." });
            }

            var comment = new Comment
            {
                PostId = postId,
                Content = content,
                UserId = userIdValue
            };

            string userIdVal = _postService.GetUserIdByPostId(postId);
            var user = await _userManager.FindByIdAsync(userIdValue);

            await _notificationService.AddNotificationAsync(userIdVal, $"{user.UserName} has commented on your post: {comment.Content}", postId);
            await _commentService.AddCommentAsync(comment);

            List<CommentsDetailViewModel> comments = new List<CommentsDetailViewModel>();
            var onlycomments = await _commentService.ViewCommentsWithUserDetailsAsync(postId);
            string idOfPoster = _postService.GetUserIdByPostId(postId);
            foreach (Comment commentq in onlycomments)
            {
               bool check = false;
              ;
                if ((comment.User.Id == userIdValue) || (idOfPoster == userIdValue))
                {
                    check = true;
                }
                comments.Add(new CommentsDetailViewModel() { User = commentq.User, Comment = commentq ,CanUserDelete= check});
            }
            if (comments == null)
            {
                comments = new List<CommentsDetailViewModel>();
            }
          
            ///  return View(comments);
            // Return the updated comments section as a partial view
            return PartialView("_CommentsPartial", comments);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int CommentId, int PostId)
        {
            try
            {
                await _commentService.DeleteComment(CommentId);

                // Get updated comments list
                var comments = await _commentService.ViewCommentsAsync(PostId);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        //[HttpGet]
        //public async Task<IActionResult> DeleteComment(int CommentId, int PostId)
        //{
        //    await _commentService.DeleteComment(CommentId);
        //    Post post = _postService.GetPostById(PostId);
        //    List<CommentsDetailViewModel> comments = await _commentService.ViewCommentsWithUserDetailsAsync(PostId);
        //    if (comments == null)
        //    {
        //        comments = new List<CommentsDetailViewModel>();
        //    }
        //    return RedirectToAction("AddComment", new { postId = PostId });
        //}

    }
}

