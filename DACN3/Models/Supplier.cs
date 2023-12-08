using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
