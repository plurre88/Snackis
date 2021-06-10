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

namespace ForumWeb.Pages.Admin
{
    [Authorize(Roles ="Admin")]
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

        [BindProperty(SupportsGet = true)]
        public Guid DeleteCategoryId { get; set; }

        public List<IdentityRole> Roles { get; set; }

        public bool IsUser { get; set; }
        public bool IsAdmin { get; set; }

        public IQueryable<ForumWebUser> Users { get; set; }

        private readonly CategoryGateway _categoryGateway;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserManager<ForumWebUser> _userManager;
        public List<Category> Categories { get; set; }
        public IndexModel(RoleManager<IdentityRole> roleManager, UserManager<ForumWebUser> userManager, CategoryGateway categoryGateway)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _categoryGateway = categoryGateway;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Roles = _roleManager.Roles.ToList();
            Users = _userManager.Users;
            Categories = await _categoryGateway.GetCategories();

            if(DeleteCategoryId != Guid.Empty)
            {
                await _categoryGateway.DeleteCategory(DeleteCategoryId);
                return RedirectToPage();
            }

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
            if(ModelState.IsValid)
            {
                await _categoryGateway.PostCategories(NewOrChangedCategory);
            }
            
            return RedirectToPage();
        }
        /*public async Task<IActionResult> OnPostDeleteAsync()
        {
            if(DeleteCategoryId != Guid.Empty)
            {
                await _categoryGateway.DeleteCategory(DeleteCategoryId);
            }
            return RedirectToPage();
        }*/
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
