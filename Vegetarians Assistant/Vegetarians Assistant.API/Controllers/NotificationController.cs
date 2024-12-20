namespace Vegetarians_Assistant.API.Controllers;

using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vegetarians_Assistant.API.Helpers.Firebase;
using Vegetarians_Assistant.API.Requests;
using Vegetarians_Assistant.Services.Services.Interface.INotification;

[ApiController]
[Route("api/v1/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IFirebaseHelper _firebaseHelper;
    private readonly INotificationService _notificationService;

    public NotificationController(IFirebaseHelper firebaseHelper, INotificationService notificationService)
    {
        _firebaseHelper = firebaseHelper;
        _firebaseHelper.InitializeFirebase();
        _notificationService = notificationService;
    }


    [HttpPost("send")]
    public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return BadRequest("Token is required.");
            }

            var message = new Message()
            {
                Token = request.Token,
                Notification = new Notification()
                {
                    Title = request.Title,
                    Body = request.Body
                }
            };

            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return Ok(new { success = true, response });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, error = ex.Message });
        }
    }

    [HttpPost("/api/v1/notifications/send-notification")]
    public async Task<IActionResult> SendNotification(int userId, string notificationType, string content)
    {
        await _notificationService.SendNotificationAsync(userId, notificationType, content);
        return Ok("Notification sent.");
    }

    [HttpPut("/api/v1/notifications/update-notification-settings")]
    public async Task<IActionResult> UpdateNotificationSettings(int userId, string settingName, bool isEnabled)
    {
        await _notificationService.UpdateNotificationSettingsAsync(userId, settingName, isEnabled);
        return Ok("Notification settings updated.");
    }
}

