using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class Building
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual Area IdNavigation { get; set; } = null!;

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
