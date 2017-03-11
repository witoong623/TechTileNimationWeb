using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechTileNimation.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using TechTileNimation.Models;
using Microsoft.EntityFrameworkCore;

namespace TechTileNimation.Controllers
{
    public class HomeController : Controller
    {
        public IHostingEnvironment _env;
        public AppDbContext _context;

        public HomeController(IHostingEnvironment env, AppDbContext context)
        {
            _env = env;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var entries = await _context.SensationEntry.ToListAsync();

            return View(entries);
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [ActionName("upload")]
        [HttpPost]
        public async Task<IActionResult> UploadConfirm(UploadViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string rootpath = _env.WebRootPath;
                string uploads = Path.Combine(rootpath, "upload");
                Directory.CreateDirectory(uploads);
                string sensationSoundPath = Path.Combine(uploads, viewModel.SensationSoundFile.FileName.Remove(0, viewModel.SensationSoundFile.FileName.LastIndexOf('\\') + 1));
                string previewImagePath = Path.Combine(uploads, viewModel.ImagePreviewFile.FileName.Remove(0, viewModel.ImagePreviewFile.FileName.LastIndexOf('\\') + 1));
                string animationPath = null;

                if (viewModel.AnimationFile != null)
                {
                    animationPath = Path.Combine(uploads, viewModel.AnimationFile.FileName.Remove(0, viewModel.AnimationFile.FileName.LastIndexOf('\\') + 1));
                }

                using (var saveSound = new FileStream(sensationSoundPath, FileMode.Create, FileAccess.Write))
                {
                    await viewModel.SensationSoundFile.CopyToAsync(saveSound);
                }

                using (var savePreviewImage = new FileStream(previewImagePath, FileMode.Create, FileAccess.Write))
                {
                    await viewModel.ImagePreviewFile.CopyToAsync(savePreviewImage);
                }

                if (animationPath != null)
                {
                    using (var saveAnimation = new FileStream(animationPath, FileMode.Create, FileAccess.Write))
                    {
                        await viewModel.AnimationFile.CopyToAsync(saveAnimation);
                    }
                }

                SensationEntry entry = new SensationEntry
                {
                    Name = viewModel.Name,
                    SensationSoundLink = sensationSoundPath.Substring(sensationSoundPath.LastIndexOf('\\') - 6),
                    PreviewImageLink = previewImagePath.Substring(sensationSoundPath.LastIndexOf('\\') - 6)
                };

                if (animationPath != null)
                {
                    entry.AnimationLink = animationPath.Substring(sensationSoundPath.LastIndexOf('\\') - 6);
                }

                await _context.SensationEntry.AddAsync(entry);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(HomeController.Index));
            }

            return RedirectToAction(nameof(HomeController.Upload));
        }
    }
}
