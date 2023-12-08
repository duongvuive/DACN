using DACN3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System.Diagnostics;

namespace DACN3.Controllers
{


    [Authorize(Roles = "Admin")]


    public class HomeController : Controller
    {
        Qldevice1Context db = new Qldevice1Context();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("taikhoan")]
        public IActionResult TaiKhoan(string searchString,int? Page, string sortOrder)
        {
            int pageSize = 8;
            int pageNumber = Page == null || Page < 0 ? 1 : Page.Value;
            var lstTaiKhoan = db.AspNetUsers.ToList();
            PagedList<AspNetUser> lst = new PagedList<AspNetUser>(lstTaiKhoan, pageNumber, pageSize);
            if (!string.IsNullOrEmpty(searchString))
            {
                // Nếu có chuỗi tìm kiếm, lọc danh sách sản phẩm
                lstTaiKhoan = lstTaiKhoan.Where(p => p.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    lstTaiKhoan = lstTaiKhoan.OrderByDescending(l => l.UserName).ToList();
                    break;
                /*mặc định là từ a-z*/   
                default:
                    lstTaiKhoan = lstTaiKhoan.OrderBy(l => l.UserName).ToList();
                    break;

            }
            return View(lstTaiKhoan);
        }
        /*[Route("ThemTaiKhoanMoi")]
        [HttpGet]
        public IActionResult ThemTaiKhoanMoi()
        {
            ViewBag.Roles = new SelectList(db.AspNetRoles.ToList(), "Id", "Name");
            return View();
        }
        [Route("ThemTaiKhoanMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTaiKhoanMoi(AspNetUser user)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(user);
                db.SaveChanges();
                return RedirectToAction("TaiKhoan");
            }
            return View(user);
        }*/
        [Route("SuaTaiKhoan")]
        [HttpGet]
        public IActionResult SuaTaiKhoan(string id)
        {
            var taiKhoan = db.AspNetUsers.Find(id);
            return View(taiKhoan);
        }
        [Route("SuaTaiKhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTaiKhoan(AspNetUser taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TaiKhoan", "Home");
            }
            return View(taiKhoan);
        }
        public IActionResult XoaTaiKhoan(string id)
        {
            TempData["Message"] = "";
            db.Remove(db.AspNetUsers.Find(id));
            db.SaveChanges();
            TempData["Message"] = "Tài khoảng này đã được xoá!";
            return RedirectToAction("TaiKhoan", "Home");
        }

        /* [Route("phanquyen")]
         public IActionResult phanquyen(int? Page)
         {
             int pageSize = 8;
             int pageNumber = Page == null || Page < 0 ? 1 : Page.Value;
             var lstphanquyen = db.AspNetRoles.ToList();
             PagedList<AspNetRole> lst = new PagedList<AspNetRole>(lstphanquyen, pageNumber, pageSize);
             return View(lstphanquyen);
         }
         [Route("ThemphanquyenMoi")]
         [HttpGet]
         public IActionResult ThemphanquyenMoi()
         {
             return View();
         }
         [Route("ThemphanquyenMoi")]
         [HttpPost]
         [ValidateAntiForgeryToken]
         public IActionResult ThemphanquyenMoi(AspNetRole user)
         {
             if (ModelState.IsValid)
             {
                 db.AspNetRoles.Add(user);
                 db.SaveChanges();
                 return RedirectToAction("phanquyen");
             }
             return View(user);
         }
         [Route("Suaphanquyen")]
         [HttpGet]
         public IActionResult Suaphanquyen(string id)
         {
             var phanquyen = db.AspNetRoles.Find(id);
             return View(phanquyen);
         }
         [Route("Suaphanquyen")]
         [HttpPost]
         [ValidateAntiForgeryToken]
         public IActionResult Suaphanquyen(AspNetRole phanquyen)
         {
             if (ModelState.IsValid)
             {
                 db.Entry(phanquyen).State = EntityState.Modified;
                 db.SaveChanges();
                 return RedirectToAction("phanquyen", "Roles");
             }
             return View(phanquyen);
         }
         public IActionResult Xoaphanquyen(string id)
         {
             TempData["Message"] = "";
             db.Remove(db.AspNetRoles.Find(id));
             db.SaveChanges();
             TempData["Message"] = "Quyền này đã được xoá!";
             return RedirectToAction("phanquyen", "Roles");
         }*/
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}