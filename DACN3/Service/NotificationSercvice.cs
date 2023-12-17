using Microsoft.AspNetCore.Identity;
using DACN3.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace DACN3.Service
{
    public class NotificationSercvice : INotificationService 
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Qldevice1Context _context;

        public NotificationSercvice (IHttpContextAccessor httpContextAccessor, Qldevice1Context context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public  void  CreateBrokenHistory(string senderID, int classDetailsID)
        {

            var newBrokenHistory = new BrokenHistory { 
                DeviceClassroomId = classDetailsID,
                Timestamp = DateTime.Now,
                SenderId = senderID,
            };
            _context.BrokenHistories.Add(newBrokenHistory);
            _context.SaveChanges();
        }
        public bool IsWareHouse(int amountStorehouse,int NumberOfRequest)
        {
            int subtraction = amountStorehouse - NumberOfRequest;
            if (subtraction < 0)
            {
                return false;
            } 
            else 
                return true;
            
        }
        public void CreateConfirm(string UserSenderID, int ConfirmationTableID, bool Status, string Reason)
        {
            if(Status== true)
            {
                var newConfirmation = new Confirmation
                {
                    IdNotification=ConfirmationTableID,
                    IdUserConfirms=UserSenderID,
                    ConfirmationStatus=Status,   
                };
                _context.Confirmations.Add(newConfirmation);
                _context.SaveChanges();
            }
            else if(Status== false) {
                var newConfirmation = new Confirmation
                {
                    IdNotification = ConfirmationTableID,
                    IdUserConfirms = UserSenderID,
                    ConfirmationStatus = Status,
                    Reason = Reason
                };
                _context.Confirmations.Add(newConfirmation);
                _context.SaveChanges();
            }
        }
        public void CreateHistoryWarehouseImport(int wareHouseID, int deviceID, string userid, int amountStorehouse) {
            var deviceWarehouse = _context.DeviceWarehouses.FirstOrDefault(x => x.IdDevice == deviceID  &&  x.IdWarehouse == wareHouseID);
            var newExportWarehouse = new ImportExportWarehouse
            {
                IdDeviceWarehouse = deviceWarehouse.Id,
                Date = DateTime.Now,
                IsImport = false,
                UserId = userid,
                Amount = amountStorehouse,
            };
            _context.ImportExportWarehouses.Add(newExportWarehouse);
            _context.SaveChanges();
        }
        public int IDWarehouse(int ClassDetailsID)
        {
            var idWarehouse = (from classDetail in _context.ClassDetails
                               where classDetail.Id == ClassDetailsID
                               join classroom in _context.Classrooms on classDetail.IdClassroom equals classroom.Id
                               join floor in _context.Floors on classroom.IdFloor equals floor.Id
                               join area in _context.Areas on floor.IdArea equals area.Id
                               join warehouse in _context.Warehouses on area.Id equals warehouse.IdArea
                               select warehouse.Id).FirstOrDefault();
            return idWarehouse;
        }


    }
}
