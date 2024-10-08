using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class NotificationSetting
{
    public int SettingId { get; set; }

    public int? UserId { get; set; }

    public bool? NewArticleNotification { get; set; }

    public bool? OrderStatusNotification { get; set; }

    public bool? PromotionNotification { get; set; }

    public bool? FollowNotification { get; set; }

    public virtual User? User { get; set; }
}
