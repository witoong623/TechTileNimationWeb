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
        [FileExtensions(Extensions = "ogv,webm")]
        public IFormFile AnimationFile { get; set; }

        [Required]
        [FileExtensions(Extensions = "png,gif,jpg,jpeg")]
        public IFormFile ImagePreviewFile { get; set; }

        [Required]
        [FileExtensions(Extensions = "mp3")]
        public IFormFile SensationSoundFile { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
