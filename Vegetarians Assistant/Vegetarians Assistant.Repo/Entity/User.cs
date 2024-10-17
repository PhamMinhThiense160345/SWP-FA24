using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Fullname { get; set; }

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? ImageUrl { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? Profession { get; set; }

    public int? DietaryPreferenceId { get; set; }

    public string? Status { get; set; }

    public int? RoleId { get; set; }

    public string? ActivityLevel { get; set; }

    public bool? IsPhoneVerified { get; set; }

    public virtual ICollection<ArticleLike> ArticleLikes { get; set; } = new List<ArticleLike>();

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual DietaryPreference? DietaryPreference { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Follower> FollowerFollowerUsers { get; set; } = new List<Follower>();

    public virtual ICollection<Follower> FollowerUsers { get; set; } = new List<Follower>();

    public virtual ICollection<Following> FollowingFollowingUsers { get; set; } = new List<Following>();

    public virtual ICollection<Following> FollowingUsers { get; set; } = new List<Following>();

    public virtual ICollection<NotificationSetting> NotificationSettings { get; set; } = new List<NotificationSetting>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }

    public virtual UserMembership? UserMembership { get; set; }
}
