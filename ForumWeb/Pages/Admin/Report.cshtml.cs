using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumWeb.Areas.Identity.Data;
using ForumWeb.Gateways;
using ForumWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForumWeb.Pages.Admin
{
    public class ReportModel : PageModel
    {
        private readonly ReportGateway _reportGateway;
        private readonly CommentGateway _commentGateway;
        private readonly PostGateway _postGateway;

        public UserManager<ForumWebUser> _userManager;

        [BindProperty]
        public Guid DeleteReportId { get; set; }
        [BindProperty]
        public Guid DeleteCommentId { get; set; }
        [BindProperty]
        public Guid DeletePostId { get; set; }

        public IQueryable<ForumWebUser> Users { get; set; }
        public List<Report> Reports { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
        public ForumWebUser MyUser { get; set; }
        public ReportModel(UserManager<ForumWebUser> userManager, ReportGateway reportGateway, CommentGateway commentGateway, PostGateway postGateway)
        {
            _userManager = userManager;
            _reportGateway = reportGateway;
            _commentGateway = commentGateway;
            _postGateway = postGateway;
        }
        public async Task<IActionResult> OnGetAsync()
        {

            Reports = await _reportGateway.GetReport();
            Posts = await _postGateway.GetAllPost();
            Comments = await _commentGateway.GetComments();
            MyUser = await _userManager.GetUserAsync(User);
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteReportAsync()
        {
            await _reportGateway.DeleteReport(DeleteReportId);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteCommentAsync()
        {
            await _commentGateway.DeleteComment(DeleteCommentId);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeletePostAsync()
        {
            await _postGateway.DeletePost(DeletePostId);
            return RedirectToPage();
        }
    }
}
