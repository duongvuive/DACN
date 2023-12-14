using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class Warehouse
{
    public Warehouse()
    {
        DeviceWarehouses = new HashSet<DeviceWarehouse>();
        ImportExportWarehouses = new HashSet<ImportExportWarehouse>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;

    public virtual ICollection<DeviceWarehouse> DeviceWarehouses { get; set; }
    public virtual ICollection<ImportExportWarehouse> ImportExportWarehouses { get; set; }
}
