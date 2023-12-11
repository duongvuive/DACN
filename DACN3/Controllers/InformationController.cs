using DACN3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DACN3.Controllers
{
    public class InformationController : Controller
    {
        private Qldevice1Context _context;
        public InformationController(Qldevice1Context context)
        {
            _context = context;
        }
        public IActionResult AccountInformation()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AccountInformation(string userId)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            userId = user.ToString();
            string name = Request.Form["FullName"];
            string phone = Request.Form["Phone"];
            string address = Request.Form["Addres"];
            var newAccountInformation = new AccountInformation
            {
                FullName = name,
                Phone = phone,
                Addres = address,
                IdAspNetUsers = userId
            };
            _context.AccountInformations.Add(newAccountInformation);
            _context.SaveChanges();
            bool IsAccountInformation = true;

            if (IsAccountInformation)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (User.IsInRole("Inventory Management"))
                {
                    return RedirectToAction("NotificationInventory", "Notification");
                }
                else if (User.IsInRole("Manager"))
                {
                    return RedirectToAction("ManagerStaff", "Manager");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản này chưa được phân quyền");
                    return View();
                }
            }
            return View();
        }
    }
}
