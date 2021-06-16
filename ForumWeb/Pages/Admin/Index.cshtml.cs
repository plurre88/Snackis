using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumWeb.Areas.Identity.Data;
using ForumWeb.Gateways;
using ForumWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ForumWeb.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public string AddUserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RemoveUserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Role { get; set; }

        public ForumWebUser CurrentUser { get; set; }

        [BindProperty]
        public string RoleName { get; set; }

        [BindProperty]
        public Category NewOrChangedCategory { get; set; }
        [BindProperty]
        public Guid DeleteCategoryId { get; set; }

        [BindProperty]
        public SubCategory NewOrChangedSubCategory { get; set; }
        [BindProperty]
        public Guid DeleteSubCategoryId { get; set; }
        public SelectList SelectList { get; set; }

        public List<IdentityRole> Roles { get; set; }

        public bool IsUser { get; set; }
        public bool IsAdmin { get; set; }

        public IQueryable<ForumWebUser> Users { get; set; }

        private readonly SubCategoryGateway _subCategoryGateway;
        private readonly CategoryGateway _categoryGateway;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ForumWebUser> _userManager;

        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public IndexModel(RoleManager<IdentityRole> roleManager, UserManager<ForumWebUser> userManager, CategoryGateway categoryGateway, SubCategoryGateway subCategoryGateway)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _categoryGateway = categoryGateway;
            _subCategoryGateway = subCategoryGateway;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Roles = _roleManager.Roles.ToList();
            Users = _userManager.Users;
            Categories = await _categoryGateway.GetCategories();
            SubCategories = await _subCategoryGateway.GetSubCategories();
            SelectList = new SelectList(Categories, "Id", "CategoryName");

            if (AddUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(AddUserId);
                var roleResult = await _userManager.AddToRoleAsync(alterUser, Role);
            }

            if(RemoveUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(RemoveUserId);
                var roleResult = await _userManager.RemoveFromRoleAsync(alterUser, Role);
            }

            CurrentUser = await _userManager.GetUserAsync(User);

            IsUser = await _userManager.IsInRoleAsync(CurrentUser, "User");
            IsAdmin = await _userManager.IsInRoleAsync(CurrentUser, "Admin");

            return Page();
        }
        public async Task<IActionResult> OnPostAsync() 
        {
            if(RoleName != null)
            {
                await CreateRole(RoleName);
            }
            if(NewOrChangedCategory != null)
            {
                await _categoryGateway.PostCategories(NewOrChangedCategory);
            }            
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostSubCreateAsync()
        {
            if (NewOrChangedSubCategory != null)
            {
                await _subCategoryGateway.PostSubCategories(NewOrChangedSubCategory);
            }
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAsync()
        {
            if (DeleteCategoryId != Guid.Empty)
            {
                await _categoryGateway.DeleteCategory(DeleteCategoryId);
            }
            if (DeleteSubCategoryId != Guid.Empty)
            {
                await _subCategoryGateway.DeleteSubCategory(DeleteSubCategoryId);
            }
            return RedirectToPage();
        }
        public async Task CreateRole(string roleName)
        {
            bool exist = await _roleManager.RoleExistsAsync(roleName);

            if(!exist)
            {
                var role = new IdentityRole
                {
                    Name = roleName
                };

                await _roleManager.CreateAsync(role);
            }
        }
    }
}
