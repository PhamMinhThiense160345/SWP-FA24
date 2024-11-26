using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Repo.Repositories.Repo;

namespace Vegetarians_Assistant.Repo.Repositories.Implement
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private VegetariansAssistantV3Context context = new VegetariansAssistantV3Context();
        private GenericRepository<Article> _articleRepository;
        private GenericRepository<ArticleImage> _articleImageRepository;
        private GenericRepository<ArticleLike> _articleLikeRepository;
        private GenericRepository<ArticleBody> _articleBodyRepository;
        private GenericRepository<Comment> _commentRepository;
        private GenericRepository<CommentImage> _commentImageRepository;
        private GenericRepository<DietaryPreference> _dietaryPreferenceRepository;
        private GenericRepository<Dish> _dishRepository;
        private GenericRepository<FavoriteDish> _favoriteDishRepository;
        private GenericRepository<Feedback> _feedbackRepository;
        private GenericRepository<FixedMenu> _fixedMenuRepository;
        private GenericRepository<FixedMenuItem> _fixedMenuItemRepository;
        private GenericRepository<Following> _followingRepository;
        private GenericRepository<Follower> _followerRepository;
        private GenericRepository<HealthRecord> _healthRecordRepository;
        private GenericRepository<MembershipTier> _membershipTierRepository;
        private GenericRepository<Notification> _notificationRepository;
        private GenericRepository<NotificationSetting> _notificationSettingRepository;
        private GenericRepository<NotificationType> _notificationTypeRepository;
        private GenericRepository<NutritionalInfo> _nutritionalInfoRepository;
        private GenericRepository<NutritionCriterion> _nutritionCriterionRepository;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<OrderDetail> _orderDetailRepository;
        private GenericRepository<PaymentDetail> _paymentDetailRepository;
        private GenericRepository<PaymentMethod> _paymentMethodRepository;
        private GenericRepository<Restaurant> _restaurantRepository;
        private GenericRepository<Role> _roleRepository;
        private GenericRepository<Status> _statusRepository;
        private GenericRepository<TotalNutritionDish> _totalNutritionDishRepository;
        private GenericRepository<UsersNutritionCriterion> _usersNutritionCriterionRepository;
        private GenericRepository<User> _userRepository;
        private GenericRepository<UserMembership> _userMembershipRepository;
        private GenericRepository<Cart> _cartRepository;
        private GenericRepository<Ingredient> _ingredientRepository;
        private GenericRepository<DishIngredient> _dishIngredientRepository;
        private GenericRepository<InvalidWord> _invalidWordRepository;
        private GenericRepository<DiscountHistory> _discountHistoryRepository;
        public UnitOfWork(VegetariansAssistantV3Context context)
        {
            this.context = context;
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        public IGenericRepository<Article> ArticleRepository => _articleRepository ?? new GenericRepository<Article>(context);
        public IGenericRepository<ArticleImage> ArticleImageRepository => _articleImageRepository ?? new GenericRepository<ArticleImage>(context);
        public IGenericRepository<ArticleLike> ArticleLikeRepository => _articleLikeRepository ?? new GenericRepository<ArticleLike>(context);
        public IGenericRepository<ArticleBody> ArticleBodyRepository => _articleBodyRepository ?? new GenericRepository<ArticleBody>(context);
        public IGenericRepository<Comment> CommentRepository => _commentRepository ?? new GenericRepository<Comment>(context);
        public IGenericRepository<CommentImage> CommentImageRepository => _commentImageRepository ?? new GenericRepository<CommentImage>(context);
        public IGenericRepository<DietaryPreference> DietaryPreferenceRepository => _dietaryPreferenceRepository ?? new GenericRepository<DietaryPreference>(context);
        public IGenericRepository<Dish> DishRepository => _dishRepository ?? new GenericRepository<Dish>(context);
        public IGenericRepository<FavoriteDish> FavoriteDishRepository => _favoriteDishRepository ?? new GenericRepository<FavoriteDish>(context);
        public IGenericRepository<Feedback> FeedbackRepository => _feedbackRepository ?? new GenericRepository<Feedback>(context);
        public IGenericRepository<FixedMenu> FixedMenuRepository => _fixedMenuRepository ?? new GenericRepository<FixedMenu>(context);
        public IGenericRepository<FixedMenuItem> FixedMenuItemRepository => _fixedMenuItemRepository ?? new GenericRepository<FixedMenuItem>(context);
        public IGenericRepository<Follower> FollowerRepository => _followerRepository ?? new GenericRepository<Follower>(context);
        public IGenericRepository<Following> FollowingRepository => _followingRepository ?? new GenericRepository<Following>(context);
        public IGenericRepository<HealthRecord> HealthRecordRepository => _healthRecordRepository ?? new GenericRepository<HealthRecord>(context);
        public IGenericRepository<MembershipTier> MembershipTierRepository => _membershipTierRepository ?? new GenericRepository<MembershipTier>(context);
        public IGenericRepository<Notification> NotificationRepository => _notificationRepository ?? new GenericRepository<Notification>(context);
        public IGenericRepository<NotificationSetting> NotificationSettingRepository => _notificationSettingRepository ?? new GenericRepository<NotificationSetting>(context);
        public IGenericRepository<NotificationType> NotificationTypeRepository => _notificationTypeRepository ?? new GenericRepository<NotificationType>(context);
        public IGenericRepository<NutritionalInfo> NutritionalInfoRepository => _nutritionalInfoRepository ?? new GenericRepository<NutritionalInfo>(context);
        public IGenericRepository<NutritionCriterion> NutritionCriterionRepository => _nutritionCriterionRepository ?? new GenericRepository<NutritionCriterion>(context);
        public IGenericRepository<Order> OrderRepository => _orderRepository ?? new GenericRepository<Order>(context);
        public IGenericRepository<OrderDetail> OrderDetailRepository => _orderDetailRepository ?? new GenericRepository<OrderDetail>(context);
        public IGenericRepository<PaymentDetail> PaymentDetailRepository => _paymentDetailRepository ?? new GenericRepository<PaymentDetail>(context);
        public IGenericRepository<PaymentMethod> PaymentMethodRepository => _paymentMethodRepository ?? new GenericRepository<PaymentMethod>(context);
        public IGenericRepository<Restaurant> RestaurantRepository => _restaurantRepository ?? new GenericRepository<Restaurant>(context);
        public IGenericRepository<Role> RoleRepository => _roleRepository ?? new GenericRepository<Role>(context);
        public IGenericRepository<Status> StatusRepository => _statusRepository ?? new GenericRepository<Status>(context);
        public IGenericRepository<TotalNutritionDish> TotalNutritionDishRepository => _totalNutritionDishRepository ?? new GenericRepository<TotalNutritionDish>(context);
        public IGenericRepository<UsersNutritionCriterion> UsersNutritionCriterionRepository => _usersNutritionCriterionRepository ?? new GenericRepository<UsersNutritionCriterion>(context);
        public IGenericRepository<User> UserRepository => _userRepository ?? new GenericRepository<User>(context);
        public IGenericRepository<UserMembership> UserMembershipRepository => _userMembershipRepository ?? new GenericRepository<UserMembership>(context);
        public IGenericRepository<Cart> CartRepository => _cartRepository ?? new GenericRepository<Cart>(context);
        public IGenericRepository<Ingredient> IngredientRepository => _ingredientRepository ?? new GenericRepository<Ingredient>(context);
        public IGenericRepository<DishIngredient> DishIngredientRepository => _dishIngredientRepository ?? new GenericRepository<DishIngredient>(context);
        public IGenericRepository<InvalidWord> InvalidWordRepository => _invalidWordRepository ?? new GenericRepository<InvalidWord>(context);
        public IGenericRepository<DiscountHistory> DiscountHistoryRepository => _discountHistoryRepository ?? new GenericRepository<DiscountHistory>(context);
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
