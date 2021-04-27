using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace meetings_and_events.Controllers
{
    public class ImageController : Controller
    {
        // GET image
        public ActionResult GetImage(string imageUrl)
        {
            if (imageUrl == null)
            {
                return null;
            }

            try
            {
                string path = "PublicImages/" + imageUrl;

                FileStream stream = new FileStream(path, FileMode.Open);
                FileStreamResult result = new FileStreamResult(stream, "image/*");
                result.FileDownloadName = imageUrl;
                
                return result;
            }
            catch ( Exception )
            {
                // Console.WriteLine(e);
            }
            return null;
        }
    }
}