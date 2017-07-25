using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using TechTileNimation.Models;

namespace TechTileNimation.Controllers
{
    public class ManagementController : Controller
    {
        private AppDbContext _context;
        private IHostingEnvironment _env;

        public ManagementController(AppDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllData()
        {
            try
            {
                var senses = await _context.SensationEntry.ToListAsync();
                string rootpath = _env.WebRootPath;

                foreach (var sense in senses)
                {
                    System.IO.File.Delete(Path.Combine(rootpath, sense.PreviewImageLink));
                    System.IO.File.Delete(Path.Combine(rootpath, sense.SensationSoundLink));

                    if (sense.AnimationLink != null)
                    {
                        System.IO.File.Delete(Path.Combine(rootpath, sense.AnimationLink));
                    }
                }

                _context.SensationEntry.RemoveRange(senses);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}