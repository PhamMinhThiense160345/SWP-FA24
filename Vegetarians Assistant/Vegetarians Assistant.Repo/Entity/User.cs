﻿namespace Vegetarians_Assistant.Repo.Entity;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

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

    public string? Goal { get; set; }

    public bool? IsPhoneVerified { get; set; }

    public bool? IsEmailVerified { get; set; }

    public virtual ICollection<ArticleBody> ArticleBodies { get; set; } = new List<ArticleBody>();

    public virtual ICollection<ArticleLike> ArticleLikes { get; set; } = new List<ArticleLike>();

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual DietaryPreference? DietaryPreference { get; set; }

    public virtual ICollection<DiscountHistory> DiscountHistories { get; set; } = new List<DiscountHistory>();

    public virtual ICollection<FavoriteDish> FavoriteDishes { get; set; } = new List<FavoriteDish>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Follower> FollowerFollowerUsers { get; set; } = new List<Follower>();

    public virtual ICollection<Follower> FollowerUsers { get; set; } = new List<Follower>();

    public virtual ICollection<Following> FollowingFollowingUsers { get; set; } = new List<Following>();

    public virtual ICollection<Following> FollowingUsers { get; set; } = new List<Following>();

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public virtual ICollection<NotificationSetting> NotificationSettings { get; set; } = new List<NotificationSetting>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<UserDeviceToken> UserDeviceTokens { get; set; } = new List<UserDeviceToken>();

    public virtual UserMembership? UserMembership { get; set; }

    public virtual ICollection<UsersNutritionCriterion> UsersNutritionCriteria { get; set; } = new List<UsersNutritionCriterion>();

    public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
}
