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
        public bool IsConfirm(int Status)
        {
            if (Status == 0)
            {
                return false;
            }
            return true;
        }



    }
}
