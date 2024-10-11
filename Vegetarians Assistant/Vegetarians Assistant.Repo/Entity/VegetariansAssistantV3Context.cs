using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class VegetariansAssistantV3Context : DbContext
{
    public VegetariansAssistantV3Context()
    {
    }

    public VegetariansAssistantV3Context(DbContextOptions<VegetariansAssistantV3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticleImage> ArticleImages { get; set; }

    public virtual DbSet<ArticleLike> ArticleLikes { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentImage> CommentImages { get; set; }

    public virtual DbSet<DietaryPreference> DietaryPreferences { get; set; }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<FixedMenu> FixedMenus { get; set; }

    public virtual DbSet<FixedMenuItem> FixedMenuItems { get; set; }

    public virtual DbSet<Follow> Follows { get; set; }

    public virtual DbSet<HealthRecord> HealthRecords { get; set; }

    public virtual DbSet<MembershipTier> MembershipTiers { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationSetting> NotificationSettings { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<NutritionalInfo> NutritionalInfos { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserMembership> UserMemberships { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=LAPTOP-SP7VTRAP\\SQLEXPRESS;Database=VegetariansAssistantV3;uid=sa;pwd=12345;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PK__Articles__CC36F66056D53722");

            entity.Property(e => e.ArticleId).HasColumnName("article_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.ModerateDate).HasColumnName("moderate_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Articles)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Articles__author__6A30C649");
        });

        modelBuilder.Entity<ArticleImage>(entity =>
        {
            entity.HasKey(e => e.ArticleImageId).HasName("PK__ArticleI__96B3A37944DD1368");

            entity.Property(e => e.ArticleImageId).HasColumnName("article_image_id");
            entity.Property(e => e.ArticleId).HasColumnName("article_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");

            entity.HasOne(d => d.Article).WithMany(p => p.ArticleImages)
                .HasForeignKey(d => d.ArticleId)
                .HasConstraintName("FK__ArticleIm__artic__7F2BE32F");
        });

        modelBuilder.Entity<ArticleLike>(entity =>
        {
            entity.HasKey(e => e.LikeId).HasName("PK__Article___992C793086D0794D");

            entity.ToTable("Article_Likes");

            entity.Property(e => e.LikeId).HasColumnName("like_id");
            entity.Property(e => e.ArticleId).HasColumnName("article_id");
            entity.Property(e => e.LikeDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("like_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Article).WithMany(p => p.ArticleLikes)
                .HasForeignKey(d => d.ArticleId)
                .HasConstraintName("FK__Article_L__artic__6E01572D");

            entity.HasOne(d => d.User).WithMany(p => p.ArticleLikes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Article_L__user___6EF57B66");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__E795768751CCDE38");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.ArticleId).HasColumnName("article_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.PostDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("post_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Article).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__articl__74AE54BC");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__user_i__75A278F5");
        });

        modelBuilder.Entity<CommentImage>(entity =>
        {
            entity.HasKey(e => e.CommentImageId).HasName("PK__CommentI__23132B962C8DD415");

            entity.Property(e => e.CommentImageId).HasColumnName("comment_image_id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentImages)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FK__CommentIm__comme__02084FDA");
        });

        modelBuilder.Entity<DietaryPreference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Dietary___3213E83F61BF42EF");

            entity.ToTable("Dietary_Preferences");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PreferenceName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("preference_name");
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.DishId).HasName("PK__Dishes__9F2B4CF94C81EBA4");

            entity.Property(e => e.DishId).HasColumnName("dish_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DietaryPreferenceId).HasColumnName("dietary_preference_id");
            entity.Property(e => e.DishType)
                .HasMaxLength(50)
                .HasColumnName("dish_type");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.Ingredients).HasColumnName("ingredients");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Recipe).HasColumnName("recipe");

            entity.HasOne(d => d.DietaryPreference).WithMany(p => p.Dishes)
                .HasForeignKey(d => d.DietaryPreferenceId)
                .HasConstraintName("FK__Dishes__dietary___5441852A");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__7A6B2B8C2C7EE930");

            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.DishId).HasColumnName("dish_id");
            entity.Property(e => e.FeedbackContent)
                .HasColumnType("text")
                .HasColumnName("feedback_content");
            entity.Property(e => e.FeedbackDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("feedback_date");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Rating)
                .HasColumnType("decimal(2, 1)")
                .HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Dish).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.DishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedbacks__dish___7A672E12");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedbacks__order__7C4F7684");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedbacks__user___7B5B524B");
        });

        modelBuilder.Entity<FixedMenu>(entity =>
        {
            entity.HasKey(e => e.FixedMenuId).HasName("PK__Fixed_Me__88C89FD1CB40922D");

            entity.ToTable("Fixed_Menus");

            entity.Property(e => e.FixedMenuId).HasColumnName("fixed_menu_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<FixedMenuItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Fixed_Me__3213E83FB284DE31");

            entity.ToTable("Fixed_Menu_Items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DishId).HasColumnName("dish_id");
            entity.Property(e => e.FixedMenuId).HasColumnName("fixed_menu_id");
            entity.Property(e => e.MealTime)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("meal_time");

            entity.HasOne(d => d.Dish).WithMany(p => p.FixedMenuItems)
                .HasForeignKey(d => d.DishId)
                .HasConstraintName("FK__Fixed_Men__dish___5CD6CB2B");

            entity.HasOne(d => d.FixedMenu).WithMany(p => p.FixedMenuItems)
                .HasForeignKey(d => d.FixedMenuId)
                .HasConstraintName("FK__Fixed_Men__fixed__5BE2A6F2");
        });

        modelBuilder.Entity<Follow>(entity =>
        {
            entity.HasKey(e => e.FollowId).HasName("PK__Follows__15A69144DBCAE213");

            entity.Property(e => e.FollowId).HasColumnName("follow_id");
            entity.Property(e => e.FollowDate)
                .HasColumnType("datetime")
                .HasColumnName("follow_date");
            entity.Property(e => e.FollowedId).HasColumnName("followed_id");
            entity.Property(e => e.FollowerId).HasColumnName("follower_id");

            entity.HasOne(d => d.Followed).WithMany(p => p.FollowFolloweds)
                .HasForeignKey(d => d.FollowedId)
                .HasConstraintName("FK__Follows__followe__17036CC0");

            entity.HasOne(d => d.Follower).WithMany(p => p.FollowFollowers)
                .HasForeignKey(d => d.FollowerId)
                .HasConstraintName("FK__Follows__followe__160F4887");
        });

        modelBuilder.Entity<HealthRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__HealthRe__BFCFB4DD0B7D4F97");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.Height)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("height");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.RecordDate).HasColumnName("record_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("weight");

            entity.HasOne(d => d.User).WithMany(p => p.HealthRecords)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HealthRec__user___1332DBDC");
        });

        modelBuilder.Entity<MembershipTier>(entity =>
        {
            entity.HasKey(e => e.TierId).HasName("PK__Membersh__9D52AF9C263445B7");

            entity.ToTable("Membership_Tiers");

            entity.Property(e => e.TierId).HasColumnName("tier_id");
            entity.Property(e => e.DiscountRate)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("discount_rate");
            entity.Property(e => e.RequiredPoints).HasColumnName("required_points");
            entity.Property(e => e.TierName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tier_name");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842F6385CE7D");

            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.NotificationTypeId).HasColumnName("notification_type_id");
            entity.Property(e => e.SentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("sent_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("unread")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.NotificationType).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NotificationTypeId)
                .HasConstraintName("FK__Notificat__notif__09A971A2");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__user___08B54D69");
        });

        modelBuilder.Entity<NotificationSetting>(entity =>
        {
            entity.HasKey(e => e.SettingId).HasName("PK__Notifica__256E1E32EAB80E4E");

            entity.ToTable("Notification_Settings");

            entity.Property(e => e.SettingId).HasColumnName("setting_id");
            entity.Property(e => e.FollowNotification)
                .HasDefaultValue(true)
                .HasColumnName("follow_notification");
            entity.Property(e => e.NewArticleNotification)
                .HasDefaultValue(true)
                .HasColumnName("new_article_notification");
            entity.Property(e => e.OrderStatusNotification)
                .HasDefaultValue(true)
                .HasColumnName("order_status_notification");
            entity.Property(e => e.PromotionNotification)
                .HasDefaultValue(true)
                .HasColumnName("promotion_notification");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.NotificationSettings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__user___10566F31");
        });

        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.HasKey(e => e.NotificationTypeId).HasName("PK__Notifica__0BD11F1103426224");

            entity.ToTable("Notification_Types");

            entity.Property(e => e.NotificationTypeId).HasColumnName("notification_type_id");
            entity.Property(e => e.NotificationTypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("notification_type_name");
        });

        modelBuilder.Entity<NutritionalInfo>(entity =>
        {
            entity.HasKey(e => e.NutritionalInfoId).HasName("PK__Nutritio__642D3B1C2277F5A9");

            entity.ToTable("Nutritional_Info");

            entity.Property(e => e.NutritionalInfoId).HasColumnName("nutritional_info_id");
            entity.Property(e => e.Calories).HasColumnName("calories");
            entity.Property(e => e.Carbs)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("carbs");
            entity.Property(e => e.DishId).HasColumnName("dish_id");
            entity.Property(e => e.Fat)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("fat");
            entity.Property(e => e.Protein)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("protein");

            entity.HasOne(d => d.Dish).WithMany(p => p.NutritionalInfos)
                .HasForeignKey(d => d.DishId)
                .HasConstraintName("FK__Nutrition__dish___571DF1D5");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__46596229F2C6EBF0");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CompletedTime)
                .HasColumnType("datetime")
                .HasColumnName("completed_time");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("delivery_address");
            entity.Property(e => e.DeliveryFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("delivery_fee");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("payment_method");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_price");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Orders__status_i__628FA481");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__user_id__619B8048");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__3764B6BC418A3328");

            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.DishId).HasColumnName("dish_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Dish).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.DishId)
                .HasConstraintName("FK__OrderItem__dish___66603565");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderItem__order__656C112C");
        });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment___ED1FC9EAA2D696A7");

            entity.ToTable("Payment_Details");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("payment_status");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("transaction_id");

            entity.HasOne(d => d.Order).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Payment_D__order__1BC821DD");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK__Payment_D__payme__1CBC4616");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__Payment___8A3EA9EBCD0E3130");

            entity.ToTable("Payment_Methods");

            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.PaymentMethodName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_method_name");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.RestaurantId).HasName("PK__Restaura__3B0FAA9177092AAC");

            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.ActivityTime)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("activity_time");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact_number");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.Website)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("website");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CC3888081D");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__3683B5318103FD5F");

            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F34DAADA1");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ActivityLevel)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("activity_level");
            entity.Property(e => e.Address)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.DietaryPreferenceId).HasColumnName("dietary_preference_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fullname");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.IsEmailVerified)
                .HasDefaultValue(false)
                .HasColumnName("is_email_verified");
            entity.Property(e => e.IsPhoneVerified)
                .HasDefaultValue(false)
                .HasColumnName("is_phone_verified");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.Profession)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("profession");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("active")
                .HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.DietaryPreference).WithMany(p => p.Users)
                .HasForeignKey(d => d.DietaryPreferenceId)
                .HasConstraintName("FK__Users__dietary_p__5070F446");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__role_id__5165187F");
        });

        modelBuilder.Entity<UserMembership>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User_Mem__B9BE370F12CC3DA1");

            entity.ToTable("User_Memberships");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.AccumulatedPoints)
                .HasDefaultValue(0)
                .HasColumnName("accumulated_points");
            entity.Property(e => e.DiscountGrantedDate)
                .HasColumnType("datetime")
                .HasColumnName("discount_granted_date");
            entity.Property(e => e.LastDiscountUsed)
                .HasColumnType("datetime")
                .HasColumnName("last_discount_used");
            entity.Property(e => e.TierId).HasColumnName("tier_id");

            entity.HasOne(d => d.Tier).WithMany(p => p.UserMemberships)
                .HasForeignKey(d => d.TierId)
                .HasConstraintName("FK__User_Memb__tier___245D67DE");

            entity.HasOne(d => d.User).WithOne(p => p.UserMembership)
                .HasForeignKey<UserMembership>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Memb__user___236943A5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
