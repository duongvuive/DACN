using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class Borrow
{
    public int BorrowId { get; set; }
    public int DeviceClassroomId { get; set; }
    public string UserId { get; set; } = null!;
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool? Status { get; set; }
    public string? Borrower { get; set; }

    public virtual ClassDetail DeviceClassroom { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
