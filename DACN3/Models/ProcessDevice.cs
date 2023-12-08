using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class ProcessDevice
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DeviceRepairProcess> DeviceRepairProcesses { get; set; } = new List<DeviceRepairProcess>();
}
