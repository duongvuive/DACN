using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class Warehouse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int IdArea { get; set; }

    public virtual ICollection<DeviceWarehouse> DeviceWarehouses { get; set; } = new List<DeviceWarehouse>();

    public virtual Area IdAreaNavigation { get; set; } = null!;
}
