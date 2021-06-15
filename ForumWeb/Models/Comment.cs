using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumWeb.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid CreatedCommentById { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CommentContent { get; set; }
        public Guid PostId { get; set; }
    }
}
