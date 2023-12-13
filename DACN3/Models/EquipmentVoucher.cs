using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class EquipmentVoucher
{
    public int Id { get; set; }

    public string IdRequester { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public string Reason { get; set; } = null!;

    public virtual ICollection<ConfirmEquipmentVoucher> ConfirmEquipmentVouchers { get; set; } = new List<ConfirmEquipmentVoucher>();

    public virtual AspNetUser IdRequesterNavigation { get; set; } = null!;
}
