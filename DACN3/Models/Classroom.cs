using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class Classroom
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IdFloor { get; set; }

    public virtual ICollection<ClassDetail> ClassDetails { get; set; } = new List<ClassDetail>();

    public virtual Floor IdFloorNavigation { get; set; } = null!;
}
