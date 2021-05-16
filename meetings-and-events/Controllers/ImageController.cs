using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace meetings_and_events.Controllers
{
    public class ImageController : Controller
    {
        [HttpPost]
        [Route("uploadimg")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var uploads = "PublicImages";
            if(!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            if (file.Length > 0) {
                var filePath = Path.Combine(uploads, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create)) {
                    await file.CopyToAsync(fileStream);
                }
            }
            return Ok();
        }
        
        [HttpGet]
        [Route("getimg")]
        public async Task<IActionResult> Getimg([FromQuery] string filename)
        {
            const string pattern = @"\.\."; // path test
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            if (filename == null || regex.IsMatch(filename))
            {
                filename = "\\testimage.jpg";
            }

            var uploads = "PublicImages";
            var filePath = Path.Combine(uploads, filename);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), filename); 
        }
        
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if(!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}