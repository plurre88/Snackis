using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumWeb.Models
{
    public class SubCategory
    {
        public Guid Id { get; set; }
        public string SubCatName { get; set; }
        public Guid CategoryId { get; set; }
    }
}
