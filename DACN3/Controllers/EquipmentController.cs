using DACN3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Security.Claims;

namespace DACN3.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly Qldevice1Context _context;

        public EquipmentController(Qldevice1Context context)
        {
            _context = context;
        }
        public IActionResult ListResons()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Lấy danh sách các "reson" của người dùng đang đăng nhập
            var resonList = _context.EquipmentVouchers
                .Where(e => e.IdRequester == userId)
                .ToList();

            return View(resonList);
        }
        public IActionResult Reson()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Reson(string userid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            userid= userId.ToString();
            string reason = Request.Form["Reason"];

            try
            {
                var newEquipmentVoucher = new EquipmentVoucher
                {
                    IdRequester = userid,
                    CreationDate = DateTime.Now,
                    Reason = reason
                };

                _context.EquipmentVouchers.Add(newEquipmentVoucher);
                _context.SaveChanges();

                // Trả về đoạn script trong một ContentResult
                var script = @"
                <script charset='UTF-8'>
                    alert('Thành công.');
                    window.location.href = '/Equipment/Reson'; // Đường dẫn đến trang Reson
                </script>";
                return Content(script, "text/html");
            }
            catch (Exception ex)
            {
                // Trả về đoạn script trong một ContentResult
                var errorScript = $@"
                <script charset='UTF-8'>
                    alert('That bai: {ex.Message}');
                    window.location.href = '/Equipment/Reson'; // Đường dẫn đến trang Reson
                </script>";
                return Content(errorScript, "text/html");
            }
        }
    }
}

