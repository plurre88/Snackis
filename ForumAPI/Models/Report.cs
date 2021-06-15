using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAPI.Models
{
    public class Report
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }
        public Guid ByUser { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
