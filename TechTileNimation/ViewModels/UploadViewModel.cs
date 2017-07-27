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
        [IFormFileExtensions(Extensions = "ogv,webm,gif")]
        public IFormFile AnimationFile { get; set; }

        [Required]
        [IFormFileExtensions(Extensions = "png,gif,jpg,jpeg")]
        public IFormFile ImagePreviewFile { get; set; }

        [Required]
        [IFormFileExtensions(Extensions = "mp3")]
        public IFormFile SensationSoundFile { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
