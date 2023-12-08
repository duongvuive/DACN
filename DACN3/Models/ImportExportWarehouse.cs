using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class ImportExportWarehouse
{
    public int Id { get; set; }

    public int IdDevice { get; set; }

    public int IdWarehouse { get; set; }

    public DateTime Date { get; set; }

    public bool IsImport { get; set; }

    public string UserId { get; set; } = null!;

    public virtual Device IdDeviceNavigation { get; set; } = null!;

    public virtual Warehouse IdWarehouseNavigation { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
