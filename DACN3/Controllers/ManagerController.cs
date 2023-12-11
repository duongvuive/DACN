using DACN3.Models;
using DACN3.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DACN3.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly Qldevice1Context _context;
        public ManagerController (Qldevice1Context context)
        {
            _context = context;
        }
        public IActionResult ManagerStaff(string username, string password)
        {
            return View();
        }
        public IActionResult CreateCreateClassDetail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateClassDetail()
        {
            int idDevice = int.Parse(Request.Form["IdDevice"]);
            int idClassroom = int.Parse(Request.Form["IdClassroom"]);
            int quantify = int.Parse(Request.Form["Quantify"]);

                var classDetail = new ClassDetail
                {
                    IdDevice = idDevice,
                    IdClassroom = idClassroom,
                    Quantify = quantify
                };
                _context.ClassDetails.Add(classDetail);
                _context.SaveChanges();
            var DeviceClassroomId=_context.ClassDetails.FirstOrDefault(x=>x.IdDevice == idDevice&& x.IdClassroom==idClassroom);
            int id = DeviceClassroomId.Id;
            TempData["ID"] = id;
            return RedirectToAction("CreateBorrow");
            /*return View(classDetail); */
        }

      /*  [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ManagerBorrow()
        {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string nameBorrower = Request.Form["Borrower"];
                int deviceClassroomId = (int)TempData["ID"];
                var borrrow = new Borrow
                {
                    DeviceClassroomId=deviceClassroomId,
                    UserId= userId,
                    BorrowDate= DateTime.Now,
                    Borrower= nameBorrower
                };


                _context.Add(borrrow);
                _context.SaveChanges();
                return View(borrrow); 
        }*/
    }
}
