using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class Notification
{
    public int Id { get; set; }

    public string ValidatorId { get; set; } = null!;

    public int IdBrokenHistory { get; set; }
    public string Information { get; set; } = null!;
    public bool Watched { get; set; }
    public virtual BrokenHistory IdBrokenHistoryNavigation { get; set; } = null!;

    public virtual AspNetUser Validator { get; set; } = null!;
}
