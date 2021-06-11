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
    public class PostViewModel : PageModel
    {
        private readonly UserManager<ForumWebUser> _userManager;
        private readonly CategoryGateway _categoryGateway;
        private readonly SubCategoryGateway _subCategoryGateway;
        private readonly PostGateway _postGateway;

        public ForumWebUser MyUser { get; set; }
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<Post> Posts { get; set; }

        [BindProperty]
        public Post NewPost { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid SelectedSubCategoryId { get; set; }

        public PostViewModel(UserManager<ForumWebUser> userManager, CategoryGateway categoryGateway, SubCategoryGateway subCategoryGateway, PostGateway postGateway)
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
            Posts = await _postGateway.GetAllPost();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage();
        }
    }
}
