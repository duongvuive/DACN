using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class DetailsEquipmentVoucher
{
    public int IdVouchers { get; set; }

    public int IdDevice { get; set; }

    public int Amount { get; set; }

    public virtual Device IdDeviceNavigation { get; set; } = null!;

    public virtual EquipmentVoucher IdVouchersNavigation { get; set; } = null!;
}
