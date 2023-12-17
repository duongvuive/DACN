using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class DeviceWarehouse
{
    public int Id { get; set; }

    public int IdDevice { get; set; }

    public int IdWarehouse { get; set; }

    public int Quantity { get; set; }

    public virtual Device IdDeviceNavigation { get; set; } = null!;

    public virtual Warehouse IdWarehouseNavigation { get; set; } = null!;

    public virtual ICollection<ImportExportWarehouse> ImportExportWarehouses { get; set; } = new List<ImportExportWarehouse>();
}
