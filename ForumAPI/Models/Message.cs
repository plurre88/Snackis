using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAPI.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid FromUser { get; set; }
        public Guid ToUser { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
