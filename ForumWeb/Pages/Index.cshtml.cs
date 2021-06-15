using ForumWeb.Areas.Identity.Data;
using ForumWeb.Gateways;
using ForumWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ForumWebUser> _userManager;
        private readonly CategoryGateway _categoryGateway;
        private readonly SubCategoryGateway _subCategoryGateway;
        private readonly PostGateway _postGateway;

        public ForumWebUser MyUser { get; set; }
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<PresentationPost> Posts { get; set; } = new List<PresentationPost>();

        [BindProperty]
        public Post NewPost { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid SelectedSubCategoryId { get; set; }

        public class PresentationPost
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public Guid SubCatId { get; set; }
            public ForumWebUser User { get; set; }
            public DateTime CreatedAt { get; set; }
            public string PostContent { get; set; }
        }

        public IndexModel(UserManager<ForumWebUser> userManager, CategoryGateway categoryGateway, SubCategoryGateway subCategoryGateway, PostGateway postGateway)
        {
            _userManager = userManager;
            _categoryGateway = categoryGateway;
            _subCategoryGateway = subCategoryGateway;
            _postGateway = postGateway;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            MyUser = await _userManager.GetUserAsync(User);

            Categories = await _categoryGateway.GetCategories();
            SubCategories = await _subCategoryGateway.GetSubCategories();
            var posts = await _postGateway.GetAllPost();

            foreach(var post in posts)
            {
                var presentationPost = new PresentationPost
                {
                    Id = post.Id,
                    Title = post.Title,
                    SubCatId = post.SubCatId,
                    User = await _userManager.FindByIdAsync(post.CreatedPostById.ToString()),
                    CreatedAt = post.CreatedAt,
                    PostContent = post.PostContent
                };
                Posts.Add(presentationPost);
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _postGateway.PostPosts(NewPost);

            return RedirectToPage(new { SelectedSubCategoryId = NewPost.SubCatId });
        }
    }
}
