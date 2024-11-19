namespace Vegetarians_Assistant.API.Controllers;

using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vegetarians_Assistant.API.Helpers.Firebase;
using Vegetarians_Assistant.API.Requests;

[ApiController]
[Route("api/v1/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IFirebaseHelper _firebaseHelper;

    public NotificationController(IFirebaseHelper firebaseHelper)
    {
        _firebaseHelper = firebaseHelper;
        _firebaseHelper.InitializeFirebase();
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
}

