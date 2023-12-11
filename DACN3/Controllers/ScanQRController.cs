using DACN3.Models;
using DACN3.Models.ViewModel;
using DACN3.Service;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DACN3.Controllers
{
    public class ScanQRController : Controller
    {
        Qldevice1Context manageDevice = new Qldevice1Context();
        private readonly INotificationService _notificationService;
        public ScanQRController(Qldevice1Context manageDevice, INotificationService notificationService)
        {
            this.manageDevice = manageDevice;
            _notificationService = notificationService;
        }

        public IActionResult Scan()
        {
            return View();
        }
        [HttpPost]
        public IActionResult actionResult([FromBody] QRModel model)
        {
            string result = model.Result;
            string[] part = result.Split('-');
            int deviceID = int.Parse(part[0]);
            int classroomID = int.Parse(part[1]);
            var deviceEntity = manageDevice.ClassDetails.FirstOrDefault(c => c.IdDevice == deviceID);
            var classEntity = manageDevice.ClassDetails.FirstOrDefault(c => c.IdClassroom == classroomID);


            if (deviceEntity == null || classEntity == null)
            {
                return Json(new { success = false, message = "Không tìm thấy dữ liệu" });
            }
            else
            {                
                return Json(new { success = true, classroomID = classroomID, deviceID = deviceID });

            }

        }

        public IActionResult DetailsDevice(int classroomID, int deviceID)
        {
            var resultID = manageDevice.ClassDetails.FirstOrDefault(c => c.IdDevice == deviceID && c.IdClassroom == classroomID);
            var targetClass = manageDevice.Classrooms.FirstOrDefault(c => c.Id == classroomID);
            var FloorId = targetClass.IdFloor;
            var tagetFloor = manageDevice.Floors.FirstOrDefault(c => c.Id == FloorId);
            var targetArea = manageDevice.Areas.FirstOrDefault(c => c.Id == tagetFloor.IdArea);
            var targetDevice = manageDevice.Devices.FirstOrDefault(c => c.Id == deviceID);
            var targetClassfication = manageDevice.DeviceClassfications.FirstOrDefault(c => c.Id == targetDevice.IdDeviceClassfication);
            var targetSupplier = manageDevice.Suppliers.FirstOrDefault(c => c.Id == targetDevice.IdSupplier);
            var targetQuantify = manageDevice.ClassDetails.FirstOrDefault(c => c.IdClassroom == classroomID && c.IdDevice == deviceID);
            var Name = $"{targetArea.Name} - {tagetFloor.Name} -{targetClass.Name}";
            TempData["DeviceID"] = resultID.Id;
            TempData["NameClass"] = Name;
            if (targetArea != null && tagetFloor != null && targetClass != null && targetDevice != null && targetClassfication != null && targetSupplier != null && targetQuantify != null)
            {
                var DetailOfDevice = new DetailOfDevice
                {
                    NameDevice = targetDevice.Name,
                    NameDeviceClassfication = targetClassfication.Name,
                    NameSupplier = targetSupplier.Name,
                    NameClass = Name,
                    Quantify = targetQuantify.Quantify

                };
                return View(DetailOfDevice);
            }
            else
            {
                return Json(new { success = false, message = "Không tìm thấy dữ liệu" });
            }
        }
    

        public IActionResult Note()
        {
            return View();
        }
        public class QRModel
        {
            public string Result { get; set; }
        }
    }
}
