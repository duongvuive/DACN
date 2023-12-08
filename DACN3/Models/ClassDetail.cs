using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class ClassDetail
{
    public int Id { get; set; }

    public int IdDevice { get; set; }

    public int IdClassroom { get; set; }

    public int Quantify { get; set; }

    public virtual ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();

    public virtual ICollection<BrokenHistory> BrokenHistories { get; set; } = new List<BrokenHistory>();

    public virtual Classroom IdClassroomNavigation { get; set; } = null!;

    public virtual Device IdDeviceNavigation { get; set; } = null!;

    public virtual ICollection<Repair> Repairs { get; set; } = new List<Repair>();
}
