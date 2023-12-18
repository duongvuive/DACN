using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class Area
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IdBuilding { get; set; }

    public virtual Building? Building { get; set; }

    public virtual ICollection<Floor> Floors { get; set; } = new List<Floor>();
}
