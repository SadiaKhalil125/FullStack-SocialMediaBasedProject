﻿namespace Bonded.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
