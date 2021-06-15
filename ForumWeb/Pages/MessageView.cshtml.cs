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

namespace ForumWeb.Pages
{
    public class MessageViewModel : PageModel
    {
        private readonly UserManager<ForumWebUser> _userManager;
        private readonly MessageGateway _messageGateway;

        public List<PresentationMessage> Messages { get; set; } = new();
        public ForumWebUser MyUser { get; set; }

        [BindProperty]
        public Message ReplyMessage { get; set; }

        public class PresentationMessage
        {
            public Guid Id { get; set; }
            public string Content { get; set; }
            public ForumWebUser FromUser { get; set; }
            public ForumWebUser ToUser { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public MessageViewModel(UserManager<ForumWebUser> userManager, MessageGateway messageGateway)
        {
            _userManager = userManager;
            _messageGateway = messageGateway;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            MyUser = await _userManager.GetUserAsync(User);
            var messages = await _messageGateway.GetMessage();

            foreach(var message in messages)
            {
                var presentationMessage = new PresentationMessage
                {
                    Id = message.Id,
                    Content = message.Content,
                    FromUser = await _userManager.FindByIdAsync(message.FromUser.ToString()),
                    ToUser = await _userManager.FindByIdAsync(message.ToUser.ToString()),
                    CreatedAt = message.CreatedAt
                };
                Messages.Add(presentationMessage);
            }

            return Page();
        }
        public async Task<IActionResult> OnPostReplyAsync()
        {
            await _messageGateway.PostMessage(ReplyMessage);
            return RedirectToPage();
        }
    }
}
