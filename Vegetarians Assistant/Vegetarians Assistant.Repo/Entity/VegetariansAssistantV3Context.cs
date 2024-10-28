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

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<DietaryPreference> DietaryPreferences { get; set; }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<DishIngredient> DishIngredients { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<FixedMenu> FixedMenus { get; set; }

    public virtual DbSet<FixedMenuItem> FixedMenuItems { get; set; }

    public virtual DbSet<Follower> Followers { get; set; }

    public virtual DbSet<Following> Followings { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<MembershipTier> MembershipTiers { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationSetting> NotificationSettings { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<NutritionCriterion> NutritionCriteria { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserMembership> UserMemberships { get; set; }

    public virtual DbSet<UsersNutritionCriterion> UsersNutritionCriteria { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:vegetarianserver.database.windows.net,1433;Initial Catalog=VegetariansAssistantV3;User ID=tripro3214;Password=Kuroko1769;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Vietnamese_CI_AS");

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PK__Articles__CC36F660020629F4");

            entity.Property(e => e.ArticleId).HasColumnName("article_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Content).HasColumnName("content");
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
                .HasConstraintName("FK__Articles__author__00DF2177");
        });

        modelBuilder.Entity<ArticleImage>(entity =>
        {
            entity.HasKey(e => e.ArticleImageId).HasName("PK__ArticleI__96B3A3799D602DA9");

            entity.Property(e => e.ArticleImageId).HasColumnName("article_image_id");
            entity.Property(e => e.ArticleId).HasColumnName("article_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");

            entity.HasOne(d => d.Article).WithMany(p => p.ArticleImages)
                .HasForeignKey(d => d.ArticleId)
                .HasConstraintName("FK__ArticleIm__artic__13F1F5EB");
        });

        modelBuilder.Entity<ArticleLike>(entity =>
        {
            entity.HasKey(e => e.LikeId).HasName("PK__Article___992C79309439F76E");

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
                .HasConstraintName("FK__Article_L__artic__04AFB25B");

            entity.HasOne(d => d.User).WithMany(p => p.ArticleLikes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Article_L__user___05A3D694");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__2EF52A271FCD952E");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.DishId).HasColumnName("dish_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Dish).WithMany(p => p.Carts)
                .HasForeignKey(d => d.DishId)
                .HasConstraintName("FK__Cart__dish_id__3B0BC30C");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Cart__user_id__3A179ED3");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__E795768778E8F0A5");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.ArticleId).HasColumnName("article_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.PostDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("post_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Article).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__articl__09746778");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__user_i__0A688BB1");
        });

        modelBuilder.Entity<DietaryPreference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Dietary___3213E83F8A97AFE8");

            entity.ToTable("Dietary_Preferences");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PreferenceName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("preference_name");
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.DishId).HasName("PK__Dishes__9F2B4CF99B9C6511");

            entity.Property(e => e.DishId).HasColumnName("dish_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DietaryPreferenceId).HasColumnName("dietary_preference_id");
            entity.Property(e => e.DishType)
                .HasMaxLength(50)
                .HasColumnName("dish_type");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Recipe).HasColumnName("recipe");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("active")
                .HasColumnName("status");

            entity.HasOne(d => d.DietaryPreference).WithMany(p => p.Dishes)
                .HasForeignKey(d => d.DietaryPreferenceId)
                .HasConstraintName("FK__Dishes__dietary___625A9A57");
        });

        modelBuilder.Entity<DishIngredient>(entity =>
        {
            entity.HasKey(e => e.DishIngredientId).HasName("PK__Dish_Ing__052926DD71EACA62");

            entity.ToTable("Dish_Ingredients");

            entity.Property(e => e.DishIngredientId).HasColumnName("dish_ingredient_id");
            entity.Property(e => e.DishId).HasColumnName("dish_id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("weight");

            entity.HasOne(d => d.Dish).WithMany(p => p.DishIngredients)
                .HasForeignKey(d => d.DishId)
                .HasConstraintName("FK__Dish_Ingr__dish___41B8C09B");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.DishIngredients)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("FK__Dish_Ingr__ingre__42ACE4D4");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__7A6B2B8CF79DAF1D");

            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.DishId).HasColumnName("dish_id");
            entity.Property(e => e.FeedbackContent).HasColumnName("feedback_content");
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
                .HasConstraintName("FK__Feedbacks__dish___0F2D40CE");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedbacks__order__11158940");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedbacks__user___10216507");
        });

        modelBuilder.Entity<FixedMenu>(entity =>
        {
            entity.HasKey(e => e.FixedMenuId).HasName("PK__Fixed_Me__88C89FD101B46C0E");

            entity.ToTable("Fixed_Menus");

            entity.Property(e => e.FixedMenuId).HasColumnName("fixed_menu_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<FixedMenuItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Fixed_Me__3213E83F6A6D2E21");

            entity.ToTable("Fixed_Menu_Items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DishId).HasColumnName("dish_id");
            entity.Property(e => e.FixedMenuId).HasColumnName("fixed_menu_id");

            entity.HasOne(d => d.Dish).WithMany(p => p.FixedMenuItems)
                .HasForeignKey(d => d.DishId)
                .HasConstraintName("FK__Fixed_Men__dish___7D0E9093");

            entity.HasOne(d => d.FixedMenu).WithMany(p => p.FixedMenuItems)
                .HasForeignKey(d => d.FixedMenuId)
                .HasConstraintName("FK__Fixed_Men__fixed__7C1A6C5A");
        });

        modelBuilder.Entity<Follower>(entity =>
        {
            entity.HasKey(e => e.FollowerId).HasName("PK__Follower__444E322FBF495676");

            entity.Property(e => e.FollowerId).HasColumnName("follower_id");
            entity.Property(e => e.FollowDate)
                .HasColumnType("datetime")
                .HasColumnName("follow_date");
            entity.Property(e => e.FollowerUserId).HasColumnName("follower_user_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.FollowerUser).WithMany(p => p.FollowerFollowerUsers)
                .HasForeignKey(d => d.FollowerUserId)
                .HasConstraintName("FK__Followers__follo__2610A626");

            entity.HasOne(d => d.User).WithMany(p => p.FollowerUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Followers__user___251C81ED");
        });

        modelBuilder.Entity<Following>(entity =>
        {
            entity.HasKey(e => e.FollowingId).HasName("PK__Followin__E8FB4889F922AC60");

            entity.Property(e => e.FollowingId).HasColumnName("following_id");
            entity.Property(e => e.FollowDate)
                .HasColumnType("datetime")
                .HasColumnName("follow_date");
            entity.Property(e => e.FollowingUserId).HasColumnName("following_user_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.FollowingUser).WithMany(p => p.FollowingFollowingUsers)
                .HasForeignKey(d => d.FollowingUserId)
                .HasConstraintName("FK__Following__follo__29E1370A");

            entity.HasOne(d => d.User).WithMany(p => p.FollowingUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Following__user___28ED12D1");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PK__Ingredie__B0E453CFF9AEE73F");

            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.Calcium)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("calcium");
            entity.Property(e => e.Calories)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("calories");
            entity.Property(e => e.Carbs)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("carbs");
            entity.Property(e => e.Cholesterol)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cholesterol");
            entity.Property(e => e.Fat)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("fat");
            entity.Property(e => e.Fiber)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("fiber");
            entity.Property(e => e.Iron)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("iron");
            entity.Property(e => e.Magnesium)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("magnesium");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Omega3)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("omega_3");
            entity.Property(e => e.Protein)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("protein");
            entity.Property(e => e.Sodium)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sodium");
            entity.Property(e => e.Sugars)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sugars");
            entity.Property(e => e.VitaminA)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("vitamin_A");
            entity.Property(e => e.VitaminB)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("vitamin_B");
            entity.Property(e => e.VitaminC)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("vitamin_C");
            entity.Property(e => e.VitaminD)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("vitamin_D");
            entity.Property(e => e.VitaminE)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("vitamin_E");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("weight");
        });

        modelBuilder.Entity<MembershipTier>(entity =>
        {
            entity.HasKey(e => e.TierId).HasName("PK__Membersh__9D52AF9C977091A4");

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
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842F5FD696DA");

            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.DeviceToken)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("device_token");
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
                .HasConstraintName("FK__Notificat__notif__1B9317B3");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__user___1A9EF37A");
        });

        modelBuilder.Entity<NotificationSetting>(entity =>
        {
            entity.HasKey(e => e.SettingId).HasName("PK__Notifica__256E1E3285C9AF53");

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
                .HasConstraintName("FK__Notificat__user___22401542");
        });

        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.HasKey(e => e.NotificationTypeId).HasName("PK__Notifica__0BD11F1103741D91");

            entity.ToTable("Notification_Types");

            entity.Property(e => e.NotificationTypeId).HasColumnName("notification_type_id");
            entity.Property(e => e.NotificationTypeName).HasColumnName("notification_type_name");
        });

        modelBuilder.Entity<NutritionCriterion>(entity =>
        {
            entity.HasKey(e => e.CriteriaId).HasName("PK__Nutritio__401F949D722266DE");

            entity.ToTable("Nutrition_Criteria");

            entity.Property(e => e.CriteriaId).HasColumnName("criteria_id");
            entity.Property(e => e.ActivityLevel).HasColumnName("activity_level");
            entity.Property(e => e.AgeRange)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("age_range");
            entity.Property(e => e.BmiRange)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("bmi_range");
            entity.Property(e => e.Calcium)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("calcium");
            entity.Property(e => e.Calories)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("calories");
            entity.Property(e => e.Carbs)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("carbs");
            entity.Property(e => e.Cholesterol)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cholesterol");
            entity.Property(e => e.Fat)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("fat");
            entity.Property(e => e.Fiber)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("fiber");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Goal).HasColumnName("goal");
            entity.Property(e => e.Iron)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("iron");
            entity.Property(e => e.Magnesium)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("magnesium");
            entity.Property(e => e.Omega3)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("omega_3");
            entity.Property(e => e.Profession).HasColumnName("profession");
            entity.Property(e => e.Protein)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("protein");
            entity.Property(e => e.Sodium)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sodium");
            entity.Property(e => e.Sugars)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sugars");
            entity.Property(e => e.VitaminA)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("vitamin_A");
            entity.Property(e => e.VitaminB)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("vitamin_B");
            entity.Property(e => e.VitaminC)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("vitamin_C");
            entity.Property(e => e.VitaminD)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("vitamin_D");
            entity.Property(e => e.VitaminE)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("vitamin_E");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__46596229A8A4C723");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CompletedTime)
                .HasColumnType("datetime")
                .HasColumnName("completed_time");
            entity.Property(e => e.DeliveryAddress).HasColumnName("delivery_address");
            entity.Property(e => e.DeliveryFailedFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("delivery_failed_fee");
            entity.Property(e => e.DeliveryFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("delivery_fee");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_price");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__user_id__6EC0713C");
        });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment___ED1FC9EA244909CF");

            entity.ToTable("Payment_Details");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("payment_method");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("payment_status");
            entity.Property(e => e.RefundAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("refund_amount");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("transaction_id");

            entity.HasOne(d => d.Order).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Payment_D__order__719CDDE7");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CC97049203");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F50178600");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ActivityLevel)
                .HasMaxLength(10)
                .HasColumnName("activity_level");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.DietaryPreferenceId).HasColumnName("dietary_preference_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Goal)
                .HasMaxLength(20)
                .HasColumnName("goal");
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
            entity.Property(e => e.Profession).HasColumnName("profession");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("active")
                .HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.DietaryPreference).WithMany(p => p.Users)
                .HasForeignKey(d => d.DietaryPreferenceId)
                .HasConstraintName("FK__Users__dietary_p__5D95E53A");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__role_id__5E8A0973");
        });

        modelBuilder.Entity<UserMembership>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User_Mem__B9BE370FA8851C13");

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
                .HasConstraintName("FK__User_Memb__tier___318258D2");

            entity.HasOne(d => d.User).WithOne(p => p.UserMembership)
                .HasForeignKey<UserMembership>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Memb__user___308E3499");
        });

        modelBuilder.Entity<UsersNutritionCriterion>(entity =>
        {
            entity.HasKey(e => e.UserNutritionCriteriaId).HasName("PK__Users_Nu__06CD70D1B95E818C");

            entity.ToTable("Users_Nutrition_Criteria");

            entity.Property(e => e.UserNutritionCriteriaId).HasColumnName("user_nutrition_criteria_id");
            entity.Property(e => e.CriteriaId).HasColumnName("criteria_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Criteria).WithMany(p => p.UsersNutritionCriteria)
                .HasForeignKey(d => d.CriteriaId)
                .HasConstraintName("FK__Users_Nut__crite__3EDC53F0");

            entity.HasOne(d => d.User).WithMany(p => p.UsersNutritionCriteria)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Users_Nut__user___3DE82FB7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
