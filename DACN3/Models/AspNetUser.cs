using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class AspNetUser
{
    public AspNetUser()
    {
        AspNetUserClaims = new HashSet<AspNetUserClaim>();
        AspNetUserLogins = new HashSet<AspNetUserLogin>();
        AspNetUserTokens = new HashSet<AspNetUserToken>();
        BrokenHistories = new HashSet<BrokenHistory>();
        Confirmations = new HashSet<Confirmation>();
        EquipmentVouchers = new HashSet<EquipmentVoucher>();
        ImportExportWarehouses = new HashSet<ImportExportWarehouse>();
        Roles = new HashSet<AspNetRole>();
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
    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }
    public virtual ICollection<BrokenHistory> BrokenHistories { get; set; }
    public virtual ICollection<Confirmation> Confirmations { get; set; }
    public virtual ICollection<EquipmentVoucher> EquipmentVouchers { get; set; }
    public virtual ICollection<ImportExportWarehouse> ImportExportWarehouses { get; set; }

    public virtual ICollection<AspNetRole> Roles { get; set; }
    public virtual ICollection<AspNetUserRole> UserRoles { get; set; } = new List<AspNetUserRole>();
}
