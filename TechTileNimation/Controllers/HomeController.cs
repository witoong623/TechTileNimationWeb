using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechTileNimation.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TechTileNimation.Controllers
{
    public class HomeController : Controller
    {
        public IHostingEnvironment _env;

        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
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
