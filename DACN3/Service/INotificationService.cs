using DACN3.Models;
using Microsoft.VisualBasic;

namespace DACN3.Service
{
    public interface INotificationService
    {
        void CreateBrokenHistory(string senderID, int deviceID);
        void CreateNotifications( int IdHistory, string information, string image);
    }
}
