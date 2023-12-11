using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DACN3.Models;

public partial class AccountInformation
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Họ và tên không được để trống.")]
    public string FullName { get; set; } = null!;
    [Required(ErrorMessage = "Số điện thoại không được để trống.")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Chỉ được nhập số.")]
    public string Phone { get; set; } = null!;
    [Required(ErrorMessage = "Địa chỉ không được để trống.")]
    public string Addres { get; set; } = null!;

    public string IdAspNetUsers { get; set; } = null!;

    public virtual AspNetUser IdAspNetUsersNavigation { get; set; } = null!;
}
