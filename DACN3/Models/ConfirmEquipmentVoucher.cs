using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class ConfirmEquipmentVoucher
{
    public int Id { get; set; }

    public int IdEquipmentVouchers { get; set; }

    public string IdUserConfirm { get; set; } = null!;

    public bool? ConfirmationStatus { get; set; }

    public string Reason { get; set; } = null!;

    public virtual EquipmentVoucher IdEquipmentVouchersNavigation { get; set; } = null!;

    public virtual AspNetUser IdUserConfirmNavigation { get; set; } = null!;
}
