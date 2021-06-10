using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumWeb.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid SubCatId { get; set; }
        public Guid CreatedPostById { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PostContent { get; set; }
    }
}
