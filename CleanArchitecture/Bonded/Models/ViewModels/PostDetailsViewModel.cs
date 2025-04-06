using Bonded.Domain;

namespace Bonded.Models.ViewModels
{
    public class PostDetailsViewModel
    {
       
            public Domain.Post Post { get; set; }
            public int LikeCount { get; set; }
            public bool LikedByUser { get; set; }
            public bool LoginnedUser{  get; set; }
    

    }
}
