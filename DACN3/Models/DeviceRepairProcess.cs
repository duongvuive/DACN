using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class DeviceRepairProcess
{
    public int Id { get; set; }

    public DateTime ChangeTime { get; set; }

    public int IdRepair { get; set; }

    public int IdProcessDevice { get; set; }

    public virtual ProcessDevice IdProcessDeviceNavigation { get; set; } = null!;

    public virtual Repair IdRepairNavigation { get; set; } = null!;
}
