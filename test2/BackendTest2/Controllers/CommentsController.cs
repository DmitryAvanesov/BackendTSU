using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackendTest2.Data;
using BackendTest2.Models;
using BackendTest2.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using BackendTest2.Services;
using Microsoft.AspNetCore.Hosting;

namespace BackendTest2.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserPermissionsService userPermissions;
        private readonly IHostingEnvironment hostingEnvironment;

        public CommentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserPermissionsService userPermissions, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.userManager = userManager;
            this.userPermissions = userPermissions;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Comments/Create
        public async Task<IActionResult> CreateAsync(Guid? photoId, String text)
        {
            if (photoId == null)
            {
                return NotFound();
            }

            if (text.Length > 0)
            {
                var user = await this.userManager.GetUserAsync(this.HttpContext.User);
                var comment = new Comment
                {
                    Created = DateTime.UtcNow,
                    Text = text,
                    UserId = user.Id,
                    User = user
                };

                var photo = await this.context.Photo
                    .Include(m => m.Comments)
                    .SingleOrDefaultAsync(m => m.Id == photoId);

                photo.Comments.Add(comment);
                this.context.Comments.Add(comment);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Details", new { photoId });
            }

            return this.View();
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await context.Comments
                .Include(c => c.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var comment = await context.Comments.SingleOrDefaultAsync(m => m.Id == id);
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CommentExists(Guid id)
        {
            return context.Comments.Any(e => e.Id == id);
        }
    }
}
