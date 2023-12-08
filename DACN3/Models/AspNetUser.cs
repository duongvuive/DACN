using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class AspNetUser
{
    public AspNetUser()
    {
        AspNetUserClaims = new List<AspNetUserClaim>();
        AspNetUserLogins = new List<AspNetUserLogin>();
        AspNetUserTokens = new List<AspNetUserToken>();
        Borrows = new List<Borrow>();
        BrokenHistories = new List<BrokenHistory>();
        ImportExportWarehouses = new List<ImportExportWarehouse>();
        Notifications = new List<Notification>();
        Repairs = new List<Repair>();
        UserRoles = new List<AspNetUserRole>();
    }
    public string Id { get; set; } = null!;

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public virtual AccountInformation? AccountInformation { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();

    public virtual ICollection<BrokenHistory> BrokenHistories { get; set; } = new List<BrokenHistory>();

    public virtual ICollection<ImportExportWarehouse> ImportExportWarehouses { get; set; } = new List<ImportExportWarehouse>();

    public virtual ICollection<Repair> Repairs { get; set; } = new List<Repair>();

    public virtual ICollection<AspNetUserRole> UserRoles { get; set; } = new List<AspNetUserRole>();
    public virtual ICollection<Notification> Notifications { get; set; }
}
