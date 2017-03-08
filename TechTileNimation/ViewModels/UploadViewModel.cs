using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechTileNimation.ViewModels
{
    public class UploadViewModel
    {
        public IFormFile AnimationFile { get; set; }
        [Required]
        public IFormFile ImagePreviewFile { get; set; }
        [Required]
        public IFormFile SensationSoundFile { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
