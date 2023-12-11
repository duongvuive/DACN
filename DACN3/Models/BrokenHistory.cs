using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class BrokenHistory
{
    public int Id { get; set; }

    public int DeviceClassroomId { get; set; }

    public DateTime Timestamp { get; set; }

    public string SenderId { get; set; } = null!;

    public virtual ClassDetail DeviceClassroom { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual AspNetUser Sender { get; set; } = null!;
}
