using AutoMapper;
using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.Services.Interface.IFirebase;
using Vegetarians_Assistant.Services.Services.Interface.INotification;

namespace Vegetarians_Assistant.Services.Services.Implement.NotificationImp
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFirebaseService _firebaseService;
        private readonly IMapper _mapper;
        public NotificationService(IUnitOfWork unitOfWork, IFirebaseService firebaseService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _firebaseService = firebaseService;
            _mapper = mapper;
        }
        public async Task SendNotificationAsync(int userId, string notificationType, string content)
        {
            // 1. Lấy device token của user
            var userDeviceTokens = await _unitOfWork.UserDeviceTokenRepository.FindAsync(t => t.UserId == userId);
            if (userDeviceTokens == null || !userDeviceTokens.Any())
            {
                // Không có device token, không gửi
                return;
            }

            // 2. Kiểm tra cài đặt thông báo của user
            var notificationSettings = await _unitOfWork.NotificationSettingRepository.FindAsync(s => s.UserId == userId);
            if (notificationSettings == null || !notificationSettings.Any())
            {
                // User chưa cài đặt thông báo, không gửi
                return;
            }

            bool shouldSendNotification = false;
            foreach (var setting in notificationSettings)
            {
                switch (notificationType)
                {
                    case "new_article":
                        shouldSendNotification = setting.NewArticleNotification ?? false;
                        break;
                    case "order_status":
                        shouldSendNotification = setting.OrderStatusNotification ?? false;
                        break;
                    case "new_promotion":
                        shouldSendNotification = setting.PromotionNotification ?? false;
                        break;
                    case "new_follower":
                        shouldSendNotification = setting.FollowNotification ?? false;
                        break;
                    //Thêm các loại thông báo khác nếu cần thiết
                    default:
                        return; // Loại thông báo không hợp lệ
                }
                // If the current setting matches the notification type and it's enabled, break and send the notification
                if (shouldSendNotification)
                {
                    break;
                }
            }

            if (!shouldSendNotification)
            {
                // Thông báo này đã tắt, không gửi
                return;
            }
            // 3. Lấy thông tin loại thông báo
            var typeNotification = await _unitOfWork.NotificationTypeRepository.FindAsync(c => c.NotificationTypeName == notificationType);
            if (typeNotification == null || !typeNotification.Any())
            {
                // loại thông báo không tồn tại
                return;
            }

            // 4. Gửi thông báo bằng Firebase
            foreach (var token in userDeviceTokens)
            {
                await _firebaseService.SendFirebaseNotificationAsync(token.DeviceToken, notificationType, content);
            }
            //5. Lưu thông báo vào database
            Repo.Entity.Notification notification = new Repo.Entity.Notification()
            {
                UserId = userId,
                NotificationTypeId = typeNotification.FirstOrDefault().NotificationTypeId,
                Content = content,
                SentDate = DateTime.Now,
                Status = "Unread"
            };
            await _unitOfWork.NotificationRepository.InsertAsync(notification);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateNotificationSettingsAsync(int userId, string settingName, bool isEnabled)
        {
            var notificationSettings = await _unitOfWork.NotificationSettingRepository.FindAsync(s => s.UserId == userId);

            if (notificationSettings == null || !notificationSettings.Any())
            {
                return;
            }

            foreach (var setting in notificationSettings)
            {
                switch (settingName)
                {
                    case "new_article":
                        setting.NewArticleNotification = isEnabled;
                        break;
                    case "order_status":
                        setting.OrderStatusNotification = isEnabled;
                        break;
                    case "new_promotion":
                        setting.PromotionNotification = isEnabled;
                        break;
                    case "new_follower":
                        setting.FollowNotification = isEnabled;
                        break;
                    default:
                        throw new ArgumentException("Invalid setting name.");
                }
            }

            // Cập nhật và lưu thay đổi
            await _unitOfWork.NotificationSettingRepository.UpdateRangeAsync(notificationSettings);
            await _unitOfWork.SaveAsync(); // Phải đảm bảo SaveAsync không bị chồng chéo
        }
    }
}
