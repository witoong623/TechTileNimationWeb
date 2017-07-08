using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechTileNimation.Models
{
    [Table("sense_entry")]
    public class SensationEntry
    {
        [Column("se_id", TypeName = "INT")]
        public int Id { get; set; }

        [Column("se_name", TypeName = "VARCHAR(50)")]
        public string Name { get; set; }

        [Column("se_image_link", TypeName = "VARCHAR(150)")]
        public string PreviewImageLink { get; set; }

        [Column("se_sound_link", TypeName = "VARCHAR(150)")]
        public string SensationSoundLink { get; set; }

        [Column("se_animation_link", TypeName = "VARCHAR(150)")]
        public string AnimationLink { get; set; }

        private string _safeName;

        [NotMapped]
        public string SafeName
        {
            get
            {
                if (_safeName == null)
                {
                    _safeName = Name.Replace(" ", "_").Replace(".", "_");
                }

                return _safeName;
            }
        }
    }
}
