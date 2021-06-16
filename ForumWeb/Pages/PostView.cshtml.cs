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
        private readonly CommentGateway _commentGateway;
        private readonly ReportGateway _reportGateway;
        private readonly MessageGateway _messageGateway;

        public ForumWebUser MyUser { get; set; }
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<PresentationComment> Comments { get; set; } = new List<PresentationComment>();

        [BindProperty]
        public Comment NewComment { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid SelectedPostId { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid SelectedCommentId { get; set; }

        [BindProperty]
        public Report SelectedReport { get; set; }
        [BindProperty]
        public Message NewMessage { get; set; }

        public PresentationPost SelectedPost { get; set; }
        [BindProperty]
        public Guid DeleteCommentId { get; set; }
        [BindProperty]
        public Guid DeletePostId { get; set; }

        public class PresentationPost
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public Guid SubCatId { get; set; }
            public ForumWebUser User { get; set; }
            public DateTime CreatedAt { get; set; }
            public string PostContent { get; set; }
        }
        public class PresentationComment
        {
            public Guid Id { get; set; }
            public ForumWebUser User { get; set; }
            public DateTime CreatedAt { get; set; }
            public string CommentContent { get; set; }
            public Guid PostId { get; set; }
        }

        public PostViewModel(UserManager<ForumWebUser> userManager, CategoryGateway categoryGateway, SubCategoryGateway subCategoryGateway, PostGateway postGateway, CommentGateway commentGateway, ReportGateway reportGateway, MessageGateway messageGateway)
        {
            _userManager = userManager;
            _categoryGateway = categoryGateway;
            _subCategoryGateway = subCategoryGateway;
            _postGateway = postGateway;
            _commentGateway = commentGateway;
            _reportGateway = reportGateway;
            _messageGateway = messageGateway;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            MyUser = await _userManager.GetUserAsync(User);

            Categories = await _categoryGateway.GetCategories();
            SubCategories = await _subCategoryGateway.GetSubCategories();
            var comments = await _commentGateway.GetComments();
            var selectedPost = await _postGateway.GetPost(SelectedPostId);

            foreach(var comment in comments)
            {
                var pressComments = new PresentationComment
                {
                    Id = comment.Id,
                    User = await _userManager.FindByIdAsync(comment.CreatedCommentById.ToString()),
                    CreatedAt = comment.CreatedAt,
                    CommentContent = comment.CommentContent,
                    PostId = comment.PostId
                };
                Comments.Add(pressComments);
            }
            SelectedPost = new PresentationPost
            {
                Id = selectedPost.Id,
                Title = selectedPost.Title,
                SubCatId = selectedPost.SubCatId,
                User = await _userManager.FindByIdAsync(selectedPost.CreatedPostById.ToString()),
                CreatedAt = selectedPost.CreatedAt,
                PostContent = selectedPost.PostContent
            };

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _commentGateway.PostComments(NewComment);

            return RedirectToPage(new { SelectedPostId = NewComment.PostId });
        }
        public async Task<IActionResult> OnPostReportAsync()
        {
            SelectedReport.Id = Guid.NewGuid();
            await _reportGateway.PostReport(SelectedReport);

            return RedirectToPage(new { SelectedPostId = SelectedPostId});
        }
        public async Task<IActionResult> OnPostMessageAsync()
        {
            NewMessage.Id = Guid.NewGuid();
            await _messageGateway.PostMessage(NewMessage);

            return RedirectToPage(new { SelectedPostId = SelectedPostId });
        }
        public async Task<IActionResult> OnPostDeleteCommentAsync()
        {
            await _commentGateway.DeleteComment(DeleteCommentId);
            return RedirectToPage(new { SelectedPostId = SelectedPostId });
        }
        public async Task<IActionResult> OnPostDeletePostAsync()
        {
            await _postGateway.DeletePost(DeletePostId);
            return RedirectToPage("/Index");
        }
    }
}
