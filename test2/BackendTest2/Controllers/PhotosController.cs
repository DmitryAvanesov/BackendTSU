using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackendTest2.Data;
using BackendTest2.Models;
using Microsoft.AspNetCore.Identity;
using BackendTest2.Services;
using BackendTest2.Models.ViewModels;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.Collections.ObjectModel;

namespace BackendTest2.Controllers
{
    public class PhotosController : Controller
    {
        private static readonly HashSet<String> AllowedExtensions = new HashSet<String> { ".jpg", ".jpeg", ".png", ".gif" };

        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserPermissionsService userPermissions;
        private readonly IHostingEnvironment hostingEnvironment;

        public PhotosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserPermissionsService userPermissions, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.userManager = userManager;
            this.userPermissions = userPermissions;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Photos
        public async Task<IActionResult> Index(Guid? tagId, String userId = "")
        {
            if (tagId == null && userId == "")
            {
                return View(await context.Photo.ToListAsync());
            }

            else if (tagId != null)
            {
                var tag = await this.context.Tags
                                .SingleOrDefaultAsync(x => x.Id == tagId);

                if (tag == null)
                {
                    return this.NotFound();
                }

                var photoTags = await this.context.PhotoTags
                    .Include(w => w.Photo)
                    .Where(x => x.TagId == tagId)
                    .ToListAsync();

                var photos = new List<Photo>();
                foreach (var item in photoTags)
                {
                    photos.Add(item.Photo);
                }

                this.ViewBag.Tag = tag.Text;
                return this.View(photos);
            }

            else
            {
                var user = await userManager.FindByIdAsync(userId);
                this.ViewBag.User = user.UserName;
                return View(await context.Photo
                    .Where(m => m.UserId == userId)
                    .ToListAsync());
            }
        }

        // GET: Photos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await context.Photo
                .Include(m => m.User)
                .Include(m => m.Tags)
                .Include(m => m.Comments)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (photo == null)
            {
                return NotFound();
            }

            if (Request.Method == "POST")
            {
                photo.Likes++;
                await this.context.SaveChangesAsync();
            }

            var tags = new List<Tag>();
            foreach (var item in photo.Tags)
            {
                var tag = await context.Tags
                    .SingleOrDefaultAsync(m => m.Id == id);

                tags.Add(tag);
            }

            this.ViewBag.Tags = tags;
            return View(photo);
        }

        // GET: Photos/Create
        public IActionResult Create()
        {
            return this.View(new PhotoViewModel());
        }

        // POST: Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhotoViewModel model)
        {
            var fileName = Path.GetFileName(ContentDispositionHeaderValue.Parse(model.File.ContentDisposition).FileName.Trim('"'));
            var fileExt = Path.GetExtension(fileName);
            if (!AllowedExtensions.Contains(fileExt))
            {
                this.ModelState.AddModelError(nameof(model.File), "This file type is prohibited");
            }

            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            if (this.ModelState.IsValid)
            {
                var photo = new Photo
                {
                    Name = model.Name,
                    Description = model.Description,
                    Likes = 0,
                    UserId = user.Id,
                    Tags = new Collection<PhotoTag>()
                };

                foreach (var currentTag in model.Tags.Split(',').Select(x => x.Trim()).Where(x => !String.IsNullOrEmpty(x)))
                {
                    var tag = await this.context.Tags
                         .Include(m => m.Photos)
                         .SingleOrDefaultAsync(m => m.Text == currentTag);

                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Text = currentTag
                        };

                        this.context.Tags.Add(tag);
                    }

                    if (tag.Photos == null)
                    {
                        tag.Photos = new Collection<PhotoTag>();
                    }


                    var photoTag = new PhotoTag
                    {
                        PhotoId = photo.Id,
                        TagId = tag.Id
                    };

                    this.context.PhotoTags.Add(photoTag);
                    photo.Tags.Add(photoTag);
                    tag.Photos.Add(photoTag);
                }

                var attachmentPath = Path.Combine(this.hostingEnvironment.WebRootPath, "attachments", photo.Id.ToString("N") + fileExt);
                photo.Path = $"/attachments/{photo.Id:N}{fileExt}";
                using (var fileStream = new FileStream(attachmentPath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read))
                {
                    await model.File.CopyToAsync(fileStream);
                }

                this.context.Add(photo);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        // GET: Photos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var photo = await this.context.Photo
                .SingleOrDefaultAsync(m => m.Id == id);
            if (photo == null)
            {
                return this.NotFound();
            }

            var model = new PhotoViewModel
            {
                Name = photo.Name,
                Description = photo.Description
            };

            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            if (User.IsInRole("Administrators") || user.Id == photo.UserId)
            {
                return View(model);
            }

            return NotFound();
        }


        // POST: Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, PhotoViewModel model)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var photo = await this.context.Photo
                .Include(m => m.Tags)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (photo == null)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                photo.Name = model.Name;
                photo.Description = model.Description;

                foreach (var currentTag in model.Tags.Split(',').Select(x => x.Trim()).Where(x => !String.IsNullOrEmpty(x)))
                {
                    var tag = await this.context.Tags
                        .Include(m => m.Photos)
                        .SingleOrDefaultAsync(m => m.Text == currentTag);

                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Text = currentTag
                        };

                        this.context.Tags.Add(tag);
                    }

                    if (tag.Photos == null)
                    {
                        tag.Photos = new Collection<PhotoTag>();
                    }

                    var photoTag = await this.context.PhotoTags
                       .SingleOrDefaultAsync(m => m.PhotoId == photo.Id && m.TagId == tag.Id);

                    if (photoTag == null)
                    {
                        photoTag = new PhotoTag
                        {
                            PhotoId = photo.Id,
                            TagId = tag.Id
                        };

                        this.context.PhotoTags.Add(photoTag);
                    }

                    photo.Tags.Add(photoTag);
                    tag.Photos.Add(photoTag);
                }

                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Details", new { id });
            }

            return this.View(model);
        }

        // GET: Photos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await context.Photo
                .SingleOrDefaultAsync(m => m.Id == id);
            if (photo == null)
            {
                return NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            if (User.IsInRole("Administrators") || user.Id == photo.UserId)
            {
                return View(photo);
            }

            return NotFound();
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var photo = await this.context.Photo.SingleOrDefaultAsync(m => m.Id == id);

            if (photo == null)
            {
                return this.NotFound();
            }

            this.context.Photo.Remove(photo);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        private bool PhotoExists(Guid id)
        {
            return context.Photo.Any(e => e.Id == id);
        }
    }
}
