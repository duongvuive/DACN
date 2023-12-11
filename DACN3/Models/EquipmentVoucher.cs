using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class EquipmentVoucher
{
    public int Id { get; set; }

    public string IdRequester { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public string Reason { get; set; } = null!;

    public virtual AspNetUser IdRequesterNavigation { get; set; } = null!;
}
