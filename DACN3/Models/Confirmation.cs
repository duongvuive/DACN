﻿using System;
using System.Collections.Generic;

namespace DACN3.Models;

public partial class Confirmation
{
    public int Id { get; set; }

    public int IdNotification { get; set; }

    public string? IdUserConfirms { get; set; }

    public bool? ConfirmationStatus { get; set; }

    public string Reason { get; set; } = null!;

    public virtual Notification IdNotificationNavigation { get; set; } = null!;

    public virtual AspNetUser? IdUserConfirmsNavigation { get; set; }
}
