using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class ImportExportWarehouse
{
    public int Id { get; set; }

    public int IdDeviceWarehouse { get; set; }

    public DateTime Date { get; set; }

    public bool IsImport { get; set; }

    public string UserId { get; set; } = null!;

    public int Amount { get; set; }

    public virtual DeviceWarehouse IdDeviceWarehouseNavigation { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
