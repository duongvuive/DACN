using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class Floor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IdArea { get; set; }

    public virtual ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();

    public virtual Area IdAreaNavigation { get; set; } = null!;
}
