using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int IdBrokenHistory { get; set; }

    public string Description { get; set; } = null!;

    public string Image { get; set; } = null!;

    public virtual ICollection<Confirmation> Confirmations { get; set; } = new List<Confirmation>();

    public virtual BrokenHistory IdBrokenHistoryNavigation { get; set; } = null!;
}
