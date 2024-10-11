using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;

namespace Vegetarians_Assistant.Repo.Repositories.Interface
{
    public interface IUnitOfWork
    {
        IGenericRepository<Article> ArticleRepository { get; }
        IGenericRepository<ArticleImage> ArticleImageRepository { get; }
        IGenericRepository<ArticleLike> ArticleLikeRepository { get; }
        IGenericRepository<Comment> CommentRepository { get; }
        IGenericRepository<CommentImage> CommentImageRepository { get; }
        IGenericRepository<DietaryPreference> DietaryPreferenceRepository { get; }
        IGenericRepository<Dish> DishRepository { get; }
        IGenericRepository<Feedback> FeedbackRepository { get; }
        IGenericRepository<FixedMenu> FixedMenuRepository { get; }
        IGenericRepository<FixedMenuItem> FixedMenuItemRepository { get; }
        IGenericRepository<Follow> FollowRepository { get; }
        IGenericRepository<HealthRecord> HealthRecordRepository { get; }
        IGenericRepository<MembershipTier> MembershipTierRepository { get; }
        IGenericRepository<Notification> NotificationRepository { get; }
        IGenericRepository<NotificationSetting> NotificationSettingRepository { get; }
        IGenericRepository<NotificationType> NotificationTypeRepository { get; }
        IGenericRepository<NutritionalInfo> NutritionalInfoRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<OrderItem> OrderItemRepository { get; }
        IGenericRepository<PaymentDetail> PaymentDetailRepository { get; }
        IGenericRepository<PaymentMethod> PaymentMethodRepository { get; }
        IGenericRepository<Restaurant> RestaurantRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        IGenericRepository<Status> StatusRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<UserMembership> UserMembershipRepository { get; }
        Task SaveAsync();
    }
}
