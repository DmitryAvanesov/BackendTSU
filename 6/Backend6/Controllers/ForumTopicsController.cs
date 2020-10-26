using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend6.Data;
using Backend6.Models;
using Backend6.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Backend6.Services;

namespace Backend6.Controllers
{
    public class ForumTopicsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserPermissionsService userPermissions;

        public ForumTopicsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserPermissionsService userPermissions)
        {
            this.context = context;
            this.userManager = userManager;
            this.userPermissions = userPermissions;
        }

        // GET: ForumTopics
        public async Task<IActionResult> Index(Guid? forumId)
        {
            var items = await this.context.ForumTopics
                .Include(p => p.Forum)
                .Include(p => p.User)
                .Include(p => p.Messages)
                .Where(p => p.ForumId == forumId)
                .ToListAsync();

            var forum = await this.context.Forums.SingleOrDefaultAsync(m => m.Id == forumId);
            this.ViewBag.Forum = forum;
            return this.View(items);
        }

        // GET: ForumTopics/Create
        public IActionResult Create(Guid? forumId)
        {
            if (forumId == null)
            {
                return this.NotFound();
            }

            this.ViewBag.ForumId = (Guid)forumId;
            return this.View();
        }

        // POST: ForumTopics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid? forumId, ForumTopicViewModel model)
        {
            if (forumId == null)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            if (this.ModelState.IsValid && user != null)
            {
                var forumTopic = new ForumTopic
                {
                    Created = DateTime.UtcNow,
                    Name = model.Name,
                    ForumId = (Guid)forumId,
                    UserId = user.Id
                };

                this.context.ForumTopics.Add(forumTopic);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index", new { forumId });
            }

            return this.View(model);
        }

        // GET: ForumTopics/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var forumTopic = await this.context.ForumTopics.SingleOrDefaultAsync(m => m.Id == id);
            if (forumTopic == null)
            {
                return this.NotFound();
            }

            var model = new ForumTopicViewModel
            {
                Name = forumTopic.Name
            };

            var forumCategories = await this.context.ForumCategories.OrderBy(x => x.Name).ToListAsync();
            this.ViewBag.ForumId = forumTopic.ForumId;
            this.ViewBag.UserId = forumTopic.UserId;
            return this.View(model);
        }

        // POST: ForumTopics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Guid? forumId, ForumTopicViewModel model)
        {
            if (id == null || forumId == null)
            {
                return this.NotFound();
            }

            var forumTopic = await this.context.ForumTopics.SingleOrDefaultAsync(m => m.Id == id);
            if (forumTopic == null)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                forumTopic.Name = model.Name;

                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index", new { forumId });
            }

            return this.View(model);
        }

        // GET: ForumTopics/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumTopic = await context.ForumTopics
                .Include(f => f.Forum)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (forumTopic == null)
            {
                return NotFound();
            }

            return View(forumTopic);
        }

        // POST: ForumTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id, Guid? forumId)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var forumTopic = await this.context.ForumTopics.SingleOrDefaultAsync(m => m.Id == id);

            if (forumTopic == null)
            {
                return this.NotFound();
            }

            this.context.ForumTopics.Remove(forumTopic);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index", new { forumId });
        }

        private bool ForumTopicExists(Guid id)
        {
            return context.ForumTopics.Any(e => e.Id == id);
        }
    }
}
