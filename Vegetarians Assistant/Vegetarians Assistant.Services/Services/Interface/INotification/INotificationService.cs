using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.Services.Interface.INotification
{
    public interface INotificationService
    {
        Task SendNotificationAsync(int userId, string notificationType, string content);
        Task UpdateNotificationSettingsAsync(int userId, string settingName, bool isEnabled);
    }
}
