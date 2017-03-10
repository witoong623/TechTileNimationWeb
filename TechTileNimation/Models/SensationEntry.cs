using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechTileNimation.Models
{
    public class SensationEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PreviewImageLink { get; set; }
        public string SensationSoundLink { get; set; }
        public string AnimationLink { get; set; }

        private string _safeName;

        [NotMapped]
        public string SafeName
        {
            get
            {
                if (_safeName == null)
                {
                    _safeName = Name.Replace(" ", "_");
                }

                return _safeName;
            }
        }
    }
}
