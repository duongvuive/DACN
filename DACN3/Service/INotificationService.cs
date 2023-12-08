using DACN3.Models;
using Microsoft.VisualBasic;

namespace DACN3.Service
{
    public interface INotificationService
    {
        void CreateBrokenHistory(string senderID, int deviceID);
        void CreateNotifications(string IdUser, int IdHistory,string information);
    }
}
