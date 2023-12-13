using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DACN3.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebApplication2.Controllers
{
    public class CameraController : Controller
    {
        private readonly Qldevice1Context _context;
        private readonly IHostingEnvironment _environment;

        public CameraController(Qldevice1Context context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Capture()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Capture(string name)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = file.FileName;
                            var fileNameToStore = string.Concat(Convert.ToString(Guid.NewGuid()), Path.GetExtension(fileName));
                            //  Path to store the snapshot in local folder
                            var filepath = Path.Combine(_environment.WebRootPath, "CameraPhotos") + $@"\{fileNameToStore}";
                            TempData["image"] = filepath;
                            // Save image file in local folder
                            if (!string.IsNullOrEmpty(filepath))
                            {
                                using (FileStream fileStream = System.IO.File.Create(filepath))
                                {
                                    file.CopyTo(fileStream);
                                    fileStream.Flush();
                                }
                            }

                            // Save image file in database
                            var imgBytes = System.IO.File.ReadAllBytes(filepath);
                            if (imgBytes != null)
                            {
                                if (imgBytes != null)
                                {
                                    string base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                                    string imageUrl = string.Concat("data:image/jpg;base64,", base64String);

                                }
                            }
                        }
                    }
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
