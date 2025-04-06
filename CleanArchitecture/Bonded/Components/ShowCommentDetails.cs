using Bonded.Models.ViewModels;
using Bonded.Domain;
using Microsoft.AspNetCore.Mvc;

using Bonded.Application.Services;

namespace Bonded.Components
{
    public class ShowCommentDetails:ViewComponent
    {
        private readonly CommentService _commentService;
        private readonly PostService _postService;

        public ShowCommentDetails(CommentService commentService,PostService postService)
        {
          _commentService = commentService;
            _postService = postService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            // Fetch the post and its comments
            string? userId = HttpContext.Session.GetString("UserId");
            string userIdValue = userId ?? "";
            string idOfPoster = _postService.GetUserIdByPostId(postId);

            List<CommentsDetailViewModel> comments = new List<CommentsDetailViewModel>();
            var onlycomments = await _commentService.ViewCommentsWithUserDetailsAsync(postId);
           
            foreach (Comment commentq in onlycomments)
            {
                bool check = false;
                ;
                if ((commentq.User.Id == userIdValue) || (idOfPoster == userIdValue))
                {
                    check = true;
                }
                comments.Add(new CommentsDetailViewModel() { User = commentq.User, Comment = commentq, CanUserDelete = check });
            }
            if (comments == null)
            {
                comments = new List<CommentsDetailViewModel>();
            }
         
            return View(comments);
       
        }
    }
}
