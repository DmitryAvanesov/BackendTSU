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
using Backend6.Models.ViewModels;

namespace Backend6.Controllers
{
    public class ForumMessagesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserPermissionsService userPermissions;

        public ForumMessagesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserPermissionsService userPermissions)
        {
            this.context = context;
            this.userManager = userManager;
            this.userPermissions = userPermissions;
        }

        // GET: ForumMessages
        public async Task<IActionResult> Index(Guid? topicId)
        {
            var items = await this.context.ForumMessages
                .Include(p => p.Topic)
                .Include(p => p.User)
                .Include(p => p.Attachments)
                .Where(p => p.TopicId == topicId)
                .ToListAsync();

            var topic = await this.context.ForumTopics.SingleOrDefaultAsync(m => m.Id == topicId);
            this.ViewBag.Topic = topic;
            this.ViewBag.Forum = await this.context.Forums.SingleOrDefaultAsync(m => m.Id == topic.ForumId);
            return this.View(items);
        }

        // GET: ForumMessages/Create
        public IActionResult Create(Guid? topicId)
        {
            if (topicId == null)
            {
                return this.NotFound();
            }

            this.ViewBag.TopicId = (Guid)topicId;
            return this.View();
        }

        // POST: ForumMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid? topicId, ForumMessageViewModel model)
        {
            if (topicId == null)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            if (this.ModelState.IsValid && user != null)
            {
                var forumMessage = new ForumMessage
                {
                    Created = DateTime.UtcNow,
                    Modified = DateTime.UtcNow,
                    Text = model.Text,
                    TopicId = (Guid)topicId,
                    UserId = user.Id
                };

                this.context.ForumMessages.Add(forumMessage);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index", new { topicId });
            }

            return this.View(model);
        }

        // GET: ForumMessages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var forumMessage = await this.context.ForumMessages.SingleOrDefaultAsync(m => m.Id == id);
            if (forumMessage == null)
            {
                return this.NotFound();
            }

            var model = new ForumMessageViewModel
            {
                Text = forumMessage.Text
            };

            this.ViewBag.TopicId = forumMessage.TopicId;
            this.ViewBag.UserId = forumMessage.UserId;
            return this.View(model);
        }

        // POST: ForumMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Guid? topicId, ForumMessageViewModel model)
        {
            if (id == null || topicId == null)
            {
                return this.NotFound();
            }

            var forumMessage = await this.context.ForumMessages.SingleOrDefaultAsync(m => m.Id == id);
            if (forumMessage == null)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                forumMessage.Text = model.Text;
                forumMessage.Modified = DateTime.UtcNow;
                await this.context.SaveChangesAsync();

                return this.RedirectToAction("Index", new { topicId });
            }

            return this.View(model);
        }

        // GET: ForumMessages/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumMessage = await context.ForumMessages
                .Include(f => f.Topic)
                .Include(f => f.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (forumMessage == null)
            {
                return NotFound();
            }

            return View(forumMessage);
        }

        // POST: ForumMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id, Guid? topicId)
        {
            if (id == null || topicId == null)
            {
                return this.NotFound();
            }

            var forumMessage = await this.context.ForumMessages.SingleOrDefaultAsync(m => m.Id == id);

            if (forumMessage == null)
            {
                return this.NotFound();
            }

            this.context.ForumMessages.Remove(forumMessage);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index", new { topicId });
        }

        private bool ForumMessageExists(Guid id)
        {
            return context.ForumMessages.Any(e => e.Id == id);
        }
    }
}
