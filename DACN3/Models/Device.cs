using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class Device
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IdDeviceClassfication { get; set; }

    public int IdSupplier { get; set; }

    public virtual ICollection<ClassDetail> ClassDetails { get; set; } = new List<ClassDetail>();

    public virtual ICollection<DeviceWarehouse> DeviceWarehouses { get; set; } = new List<DeviceWarehouse>();

    public virtual DeviceClassfication IdDeviceClassficationNavigation { get; set; } = null!;

    public virtual Supplier IdSupplierNavigation { get; set; } = null!;
}
