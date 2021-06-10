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

        public ForumWebUser MyUser { get; set; }
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }

        public Category NewOrChangedCategory { get; set; }

        public IndexModel(UserManager<ForumWebUser> userManager, CategoryGateway categoryGateway, SubCategoryGateway subCategoryGateway)
        {
            _userManager = userManager;
            _categoryGateway = categoryGateway;
            _subCategoryGateway = subCategoryGateway;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            MyUser = await _userManager.GetUserAsync(User);

            Categories = await _categoryGateway.GetCategories();
            SubCategories = await _subCategoryGateway.GetSubCategories();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            

            return RedirectToPage();
        }
    }
}
