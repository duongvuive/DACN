using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class Repair
{
    public int Id { get; set; }

    public int DeviceClassroomId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? RepairSolution { get; set; }

    public string UserId { get; set; } = null!;

    public virtual ClassDetail DeviceClassroom { get; set; } = null!;

    public virtual ICollection<DeviceRepairProcess> DeviceRepairProcesses { get; set; } = new List<DeviceRepairProcess>();

    public virtual AspNetUser User { get; set; } = null!;
}
