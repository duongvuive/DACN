using DACN3.Models;
using Microsoft.VisualBasic;

namespace DACN3.Service
{
    public interface INotificationService
    {
        void CreateBrokenHistory(string senderID, int classDetailsID);
        bool IsConfirm(int Status);
    }
}
