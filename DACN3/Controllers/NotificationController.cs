﻿using DACN3.Hubs;
using DACN3.Models;
using DACN3.Models.ViewModel;
using DACN3.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using PagedList;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using static DACN3.Controllers.ScanQRController;

namespace DACN3.Controllers
{
    public class NotificationController : Controller
    {
        private Qldevice1Context _context;
        private readonly INotificationService _notificationSercvice;
        private readonly IHubContext<NotificationHubs> _hubContext;
        public NotificationController(INotificationService notificationSercvice, Qldevice1Context context, IHubContext<NotificationHubs> hubContext)
        {
            _notificationSercvice = notificationSercvice;
            _context = context;
            _hubContext = hubContext;
        }
        public class AmountModel
        {
            public int amount { get; set; }
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Report()
        {
            int deviceID = int.Parse(TempData["DeviceID"].ToString());
            var targetDevice = _context.Devices.FirstOrDefault(c => c.Id == deviceID);
            var Image = TempData["image"] as string;
            var storedName = TempData["NameClass"] as string;
            if (deviceID != null)
            {
                var newViewDeviceBroken = new DeviceBroken
                {
                    Image = Image,
                    NameDevice = targetDevice.Name,
                    Classroom = storedName,
                    Amount = 1,
                };
                TempData["imageNotification"] = Image;
                TempData["NameClassNotification"] = storedName;
                return View(newViewDeviceBroken);
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult Confirm([FromBody] AmountModel model)
        {
            int amount = model.amount;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                string id_sender = userId.ToString();
                var account = _context.AspNetUsers.FirstOrDefault(x => x.Id == userId);
                var storedNameNotification = TempData["NameClass"] as string;
                var ImageNotification = TempData["imageNotification"] as string;
                string sender_name = account.UserName;
                int classDetailID = int.Parse(TempData["classDetailID"].ToString());
                var role = _context.AspNetRoles.FirstOrDefault(x => x.Name == "Inventory Management");
                var RoleUser = _context.AspNetUserRoles.FirstOrDefault(X => X.RoleId == role.Id);
                string Validator_ID = RoleUser.UserId.ToString();
                if (classDetailID != 0 || id_sender != null)
                {
                    _notificationSercvice.CreateBrokenHistory(id_sender, classDetailID);

                    int latestBrokenHistoryId = _context.BrokenHistories
                      .Where(b => b.SenderId == id_sender && b.DeviceClassroomId == classDetailID)
                      .OrderByDescending(b => b.Timestamp)
                      .Select(b => b.Id)
                      .FirstOrDefault();
                    var date = _context.BrokenHistories.FirstOrDefault(x => x.Id == latestBrokenHistoryId);
                    string information = $"Người gửi: {sender_name} báo cáo về thiết bị hư tại phòng học : {storedNameNotification} ngày gửi báo cáo :{date.Timestamp}";
                    CreateNotification(ImageNotification, latestBrokenHistoryId, information,amount);
                    _hubContext.Clients.User(Validator_ID).SendAsync("ReceiveNotification", information);
                    return Json(new { success = true, message = "Gửi báo cáo thành công" });
                }
            }
             return Json(new { success = false, message = "Gửi báo cáo không thành công" });
        }
        private void CreateNotification(string? Image, int latestBrokenHistoryId, string information, int amount)
        {
            if (amount > 0)
            {
                var newNotification = new Notification
                {
                    IdBrokenHistory = latestBrokenHistoryId,
                    Description = information,
                    Image = Image,
                    Amount = amount,
                };
                _context.Notifications.Add(newNotification);
                _context.SaveChanges();
            }
           
        }
        /*   [HttpPost]    
           public IActionResult Report(int amount)
           {
                 var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                 int deviceID =int.Parse(TempData["DeviceID"].ToString());

                 if (!string.IsNullOrEmpty(userId))
                   {
                           string id_sender = userId.ToString();
                           var account = _context.AspNetUsers.FirstOrDefault(x => x.Id == userId);
                           var storedName = TempData["NameClass"] as string;
                           var Image = TempData["image"] as string;
                           string sender_name = account.UserName;
                           var role = _context.AspNetRoles.FirstOrDefault(x => x.Name == "Inventory Management");
                           var RoleUser = _context.AspNetUserRoles.FirstOrDefault(X => X.RoleId == role.Id);
                           string Validator_ID = RoleUser.UserId.ToString();
                           amount = int.Parse(Request.Form["Amount"]);

                  if (deviceID != 0 || id_sender != null)
                   {
                       _notificationSercvice.CreateBrokenHistory(id_sender, deviceID);

                       int latestBrokenHistoryId = _context.BrokenHistories
                         .Where(b => b.SenderId == id_sender && b.DeviceClassroomId == deviceID)
                         .OrderByDescending(b => b.Timestamp)
                         .Select(b => b.Id)
                         .FirstOrDefault();
                       var date = _context.BrokenHistories.FirstOrDefault(x => x.Id == latestBrokenHistoryId);
                       string information = $"Người gửi: {sender_name} báo cáo về thiết bị hư tại phòng học : {storedName} ngày gửi báo cáo :{date.Timestamp}";
                       CreateNotification(Image, latestBrokenHistoryId, information);
                       var targetDevice = _context.Devices.FirstOrDefault(c => c.Id == deviceID);
                       var newViewDeviceBroken = new DeviceBroken
                       {
                           Image = Image,
                           NameDevice = targetDevice.Name,
                           Classroom = storedName,
                           Amount = 1,

                       };

                       _hubContext.Clients.User(Validator_ID).SendAsync("ReceiveNotification", information);
                       return View();
                   }
               }

               return View();
           }*/

        

        [Authorize(Roles = "Inventory Management")]
        public IActionResult NotificationInventory()
        {
            return View();
        }
       /* public IActionResult ListNotification(int? Page)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int pageSize = 8;
            int pageNumber = Page == null || Page < 0 ? 1 : Page.Value;
            var lstNotification = _context.Notifications.Where(x => x. == userId).ToList();
            PagedList<Notification> lst = new PagedList<Notification>(lstNotification, pageNumber, pageSize);
            return View(lstNotification);
        }*/
    }
}
