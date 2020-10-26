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
    public class ForaController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserPermissionsService userPermissions;

        public ForaController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserPermissionsService userPermissions)
        {
            this.context = context;
            this.userManager = userManager;
            this.userPermissions = userPermissions;
        }

        // GET: Fora
        public async Task<IActionResult> Index()
        {
            var categories = await this.context.ForumCategories
                .Include(p => p.Forums)
                .ToListAsync();

            var forums = await this.context.Forums
                .Include(p => p.Category)
                .Include(p => p.Topics)
                .ToListAsync();

            var model = new ForumCategoryForumViewModel
            {
                Categories = categories,
                Forums = forums
            };

            return this.View(model);
        }

        // GET: Fora/Create
        public IActionResult Create(Guid? categoryId)
        {
            if (categoryId == null)
            {
                return this.NotFound();
            }

            var model = new ForumViewModel();
            this.ViewBag.CategoryId = categoryId;

            return this.View(model);
        }

        // POST: Fora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid? categoryId, ForumViewModel model)
        {
            if (categoryId == null)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            if (this.ModelState.IsValid && user != null)
            {
                var now = DateTime.UtcNow;
                var forum = new Forum
                {
                    Name = model.Name,
                    Description = model.Description,
                    CategoryId = (Guid)categoryId
                };

                this.context.Forums.Add(forum);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            var foraCategories = await this.context.ForumCategories.OrderBy(x => x.Name).ToListAsync();
            this.ViewData["CategoryId"] = new SelectList(foraCategories, "Id", "Name");
            return this.View(model);
        }

        // GET: Fora/Edit/5
        public async Task<IActionResult> Edit(Guid? id, Guid? categoryId)
        {
            if (id == null || categoryId == null)
            {
                return this.NotFound();
            }

            var forum = await this.context.Forums.SingleOrDefaultAsync(m => m.Id == id);
            if (forum == null)
            {
                return this.NotFound();
            }

            var model = new ForumViewModel
            {
                Name = forum.Name,
                Description = forum.Description,
            };

            var forumCategories = await this.context.ForumCategories.OrderBy(x => x.Name).ToListAsync();
            this.ViewBag.CategoryId = categoryId;
            this.ViewBag.Forum = forum;
            return this.View(model);
        }

        // POST: Fora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Guid? categoryId, ForumViewModel model)
        {
            if (id == null || categoryId == null)
            {
                return this.NotFound();
            }

            var forum = await this.context.Forums.SingleOrDefaultAsync(m => m.Id == id);
            if (forum == null)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                forum.Name = model.Name;
                forum.Description = model.Description;
                forum.CategoryId = (Guid)categoryId;

                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        // GET: Fora/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var forum = await this.context.Forums
                .Include(p => p.Category)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (forum == null)
            {
                return this.NotFound();
            }

            return this.View(forum);
        }

        // POST: Fora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var forum = await this.context.Forums.SingleOrDefaultAsync(m => m.Id == id);

            if (forum == null)
            {
                return this.NotFound();
            }

            this.context.Forums.Remove(forum);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        private bool ForumExists(Guid id)
        {
            return context.Forums.Any(e => e.Id == id);
        }
    }
}
