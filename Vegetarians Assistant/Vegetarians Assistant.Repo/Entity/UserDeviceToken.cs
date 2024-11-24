using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class UserDeviceToken
{
    public int TokenId { get; set; }

    public int UserId { get; set; }

    public string DeviceToken { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
