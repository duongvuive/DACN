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


    }
}
