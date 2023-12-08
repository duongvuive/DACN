using DACN3.Hubs;
using DACN3.Models;
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

        [Authorize(Roles = "Manager")]
        public IActionResult Report(int deviceID)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                string id_sender = userId.ToString();
                var account = _context.AspNetUsers.FirstOrDefault(x => x.Id == userId);
                var storedName = TempData["NameClass"] as string;
                string sender_name = account.UserName;
                var role = _context.AspNetRoles.FirstOrDefault(x => x.Name == "Inventory Management");
                var RoleUser = _context.AspNetUserRoles.FirstOrDefault(X => X.RoleId == role.Id);
                string Validator_ID = RoleUser.UserId.ToString();
                string information = $"Người gửi: {sender_name} báo cáo về thiết bị hư tại phòng học : {storedName}";
                if (deviceID != 0 || id_sender != null)
                {
                    _notificationSercvice.CreateBrokenHistory(id_sender, deviceID);
                    
                    int latestBrokenHistoryId = _context.BrokenHistories
                      .Where(b => b.SenderId == id_sender && b.DeviceClassroomId == deviceID)
                      .OrderByDescending(b => b.Timestamp)
                      .Select(b => b.Id)
                      .FirstOrDefault();
                    _notificationSercvice.CreateNotifications(Validator_ID, latestBrokenHistoryId, information);
                    _hubContext.Clients.User(Validator_ID).SendAsync("ReceiveNotification", information);
                    return View();
                }
            }
            
            return View();
        }
        [Authorize(Roles = "Inventory Management")]
        public IActionResult NotificationInventory()
        {
            return View();
        }
        public IActionResult ListNotification(int? Page)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int pageSize = 8;
            int pageNumber = Page == null || Page < 0 ? 1 : Page.Value;
            var lstNotification = _context.Notifications.Where(x => x.ValidatorId == userId).ToList();
            PagedList<Notification> lst = new PagedList<Notification>(lstNotification, pageNumber, pageSize);
            return View(lstNotification);
        }
    }
}
