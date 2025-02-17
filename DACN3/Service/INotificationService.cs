﻿using DACN3.Models;
using Microsoft.VisualBasic;

namespace DACN3.Service
{
    public interface INotificationService
    {
        void CreateBrokenHistory(string senderID, int classDetailsID);
        bool IsWareHouse(int amountStorehouse, int NumberOfRequest);
        void CreateConfirm(string UserSenderID, int ConfirmationTableID, bool Status, string Reason);
        void CreateHistoryWarehouseImport(int wareHouseID,int deviceID, string userid, int amountStorehouse);
        int IDWarehouse(int ClassDetailsID);
    }
}
