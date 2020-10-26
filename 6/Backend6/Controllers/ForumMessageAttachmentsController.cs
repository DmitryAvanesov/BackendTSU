using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend6.Data;
using Backend6.Models;
using Microsoft.AspNetCore.Identity;
using Backend6.Services;
using Microsoft.AspNetCore.Hosting;
using Backend6.Models.ViewModels;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace Backend6.Controllers
{
    [Authorize]
    public class ForumMessageAttachmentsController : Controller
    {
        private static readonly HashSet<String> AllowedExtensions = new HashSet<String> { ".jpg", ".jpeg", ".png", ".gif" };

        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserPermissionsService userPermissions;
        private readonly IHostingEnvironment hostingEnvironment;

        public ForumMessageAttachmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserPermissionsService userPermissions, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.userManager = userManager;
            this.userPermissions = userPermissions;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: ForumMessageAttachments/Create
        public async Task<IActionResult> Create(Guid? messageId)
        {
            if (messageId == null)
            {
                return this.NotFound();
            }

            var message = await this.context.ForumMessages
                .SingleOrDefaultAsync(m => m.Id == messageId);
            if (message == null)
            {
                return this.NotFound();
            }

            this.ViewBag.Message = message;
            return this.View(new ForumMessageAttachmentViewModel());
        }

        // POST: ForumMessageAttachments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid? messageId, ForumMessageAttachmentViewModel model)
        {
            if (messageId == null)
            {
                return this.NotFound();
            }

            var message = await this.context.ForumMessages
                .SingleOrDefaultAsync(m => m.Id == messageId);
            if (message == null)
            {
                return this.NotFound();
            }

            var fileName = Path.GetFileName(ContentDispositionHeaderValue.Parse(model.File.ContentDisposition).FileName.Trim('"'));
            var fileExt = Path.GetExtension(fileName);
            if (!AllowedExtensions.Contains(fileExt))
            {
                this.ModelState.AddModelError(nameof(model.File), "This file type is prohibited");
            }

            if (this.ModelState.IsValid)
            {
                var forumMessageAttachment = new ForumMessageAttachment
                {
                    MessageId = message.Id,
                    Created = DateTime.UtcNow,
                    FileName = fileName
                };

                var attachmentPath = Path.Combine(this.hostingEnvironment.WebRootPath, "attachments", forumMessageAttachment.Id.ToString("N") + fileExt);
                forumMessageAttachment.FilePath = $"/attachments/{forumMessageAttachment.Id:N}{fileExt}";
                using (var fileStream = new FileStream(attachmentPath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read))
                {
                    await model.File.CopyToAsync(fileStream);
                }

                this.context.Add(forumMessageAttachment);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index", "ForumMessages", new { topicId = message.TopicId });
            }

            this.ViewBag.Message = message;
            return this.View(model);
        }

        // GET: ForumMessageAttachments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumMessageAttachment = await context.ForumMessageAttachments
                .Include(f => f.Message)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (forumMessageAttachment == null)
            {
                return NotFound();
            }

            return View(forumMessageAttachment);
        }

        // POST: ForumMessageAttachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var forumMessageAttachment = await this.context.ForumMessageAttachments
                .Include(p => p.Message)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (forumMessageAttachment == null)
            {
                return this.NotFound();
            }

            var attachmentPath = Path.Combine(this.hostingEnvironment.WebRootPath, "attachments", forumMessageAttachment.Id.ToString("N") + Path.GetExtension(forumMessageAttachment.FilePath));
            System.IO.File.Delete(attachmentPath);
            this.context.ForumMessageAttachments.Remove(forumMessageAttachment);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index", "ForumMessages", new { topicId = forumMessageAttachment.Message.TopicId });
        }
    }
}
