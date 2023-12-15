using DACN3.Hubs;
using DACN3.Models;
using DACN3.Models.ViewModel;
using DACN3.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PagedList;
using System.Security.Claims;

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
                    string information = $"Người gửi: {sender_name} báo cáo về thiết bị hư tại phòng học : {storedNameNotification}";
                    CreateNotification(ImageNotification, latestBrokenHistoryId, information, amount);
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
        [Authorize(Roles = "Inventory Management")]
        public IActionResult NotificationInventory()
        {
            return View();
        }
        [Authorize(Roles = "Inventory Management")]
        public IActionResult ListNotification(int? Page)
        {
            List<BrokenHistoryNotification> newBrokenHistoryNotifications = new List<BrokenHistoryNotification>();
            foreach (var brokenHistory in _context.BrokenHistories.ToList())
            {
                var notification = _context.Notifications.FirstOrDefault(x => x.IdBrokenHistory == brokenHistory.Id);
                var confirmationNotification = _context.Confirmations.FirstOrDefault(x => x.IdNotification == notification.Id);
                if (confirmationNotification == null)
                {
                    var newBrokenHistoryNotification = new BrokenHistoryNotification
                    {
                        Id = notification.Id,
                        Timestamp = brokenHistory.Timestamp,
                        Description = notification.Description,
                        Image = notification.Image,
                        amount = notification.Amount,
                        Status ="Tình trạng đang xét duyệt"
                    };
                    newBrokenHistoryNotifications.Add(newBrokenHistoryNotification);
                }
                
            }
            int pageSize = 8;
            int pageNumber = Page == null || Page < 0 ? 1 : Page.Value;
            PagedList<BrokenHistoryNotification> lst = new PagedList<BrokenHistoryNotification>(newBrokenHistoryNotifications, pageNumber, pageSize);
            return View(lst);
        }
        [HttpPost]
        public IActionResult ConfirmNotification(int id, bool value)
        {
            string reason = null;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _notificationSercvice.CreateConfirm(userId, id, value, reason);
            var notification = _context.Notifications.FirstOrDefault(x => x.Id == id);
            int DeviceID = _context.BrokenHistories
                    .Where(bh => bh.Id == notification.IdBrokenHistory)
                    .Join(
                        _context.ClassDetails,
                        bh => bh.DeviceClassroomId,
                        cd => cd.Id,
                        (bh, cd) => new { BrokenHistory = bh, ClassDetail = cd }
                    )
                    .Select(result => result.ClassDetail.IdDevice)
                    .FirstOrDefault();
            var Warehouse=_context.DeviceWarehouses.FirstOrDefault(x => x.IdDevice == DeviceID);
            if (_notificationSercvice.IsWareHouse(Warehouse.Quantity,notification.Amount) == false) { 
                int numverWarehouse=Warehouse.Quantity;
                int sub=notification.Amount- numverWarehouse;
                string description=$"{notification.Description} (số lượng thiết bị được duyệt {numverWarehouse} và đây là số lượng còn thiếu {sub} )";
                Warehouse.Quantity = Warehouse.Quantity- numverWarehouse;
                notification.Description = description;
                _context.SaveChanges();
            }
            else
            {
                int sub=Warehouse.Quantity-notification.Amount;
                Warehouse.Quantity = sub;
                _context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult CancelNotification(int id, bool value, string Reason)
        {
         
             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _notificationSercvice.CreateConfirm(userId, id, value, Reason);

            return Ok();
        }
    }
}
