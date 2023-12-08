using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class AccountInformation
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Addres { get; set; } = null!;

    public string IdAspNetUsers { get; set; } = null!;

    public virtual AspNetUser IdAspNetUsersNavigation { get; set; } = null!;
}
