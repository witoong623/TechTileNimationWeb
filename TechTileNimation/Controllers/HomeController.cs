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
            var viewModel = new List<ShowcaseEntryViewModel>();

            foreach (var entry in entries)
            {
                var div_css = $@"#{entry.Name}-img {{ background-image: url({entry.PreviewImageLink}); }}";
                var button_css = $@"#{entry.Name}-image-button {{ position: absolute; }}";

                viewModel.Add(new ShowcaseEntryViewModel
                {
                    Entry = entry,
                    DivCss = div_css,
                    ButtonCss = button_css
                });
            }

            return View(viewModel);
            //return View();
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
                string sensationSoundPath = Path.Combine("sensation_sound", viewModel.SensationSoundFile.FileName);
                string previewImagePath = Path.Combine("image_preview", viewModel.ImagePreviewFile.FileName);
                string animationPath = null;

                if (viewModel.AnimationFile != null)
                {
                    animationPath = Path.Combine("animation", viewModel.AnimationFile.FileName);
                }

                using (var saveSound = new FileStream(Path.Combine(rootpath, sensationSoundPath), FileMode.Create, FileAccess.Write))
                {
                    await viewModel.SensationSoundFile.CopyToAsync(saveSound);
                    Debug.WriteLine("Uploaded sound");
                }

                using (var savePreviewImage = new FileStream(Path.Combine(rootpath, previewImagePath), FileMode.Create, FileAccess.Write))
                {
                    await viewModel.ImagePreviewFile.CopyToAsync(savePreviewImage);
                    Debug.WriteLine("Uploaded image");
                }

                if (animationPath != null)
                {
                    using (var saveAnimation = new FileStream(Path.Combine(rootpath, animationPath), FileMode.Create, FileAccess.Write))
                    {
                        await viewModel.ImagePreviewFile.CopyToAsync(saveAnimation);
                        Debug.WriteLine("Uploaded animation");
                    }
                }

                return RedirectToAction(nameof(HomeController.Index));
            }

            return RedirectToAction(nameof(HomeController.Upload));
        }
    }
}
