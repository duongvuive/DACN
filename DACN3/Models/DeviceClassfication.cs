using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class DeviceClassfication
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
