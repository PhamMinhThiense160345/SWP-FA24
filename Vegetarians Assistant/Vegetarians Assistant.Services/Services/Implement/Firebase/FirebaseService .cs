using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Services.Services.Interface.IFirebase;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace Vegetarians_Assistant.Services.Services.Implement.Firebase
{
    public class FirebaseService : IFirebaseService
    {
        private readonly FirebaseMessaging _messaging;
        private readonly IConfiguration _configuration;
        public FirebaseService(IConfiguration configuration)
        {
            _configuration = configuration;
            var path = configuration.GetValue<string>("FirebaseConfig:Path");
            if (FirebaseApp.DefaultInstance == null)
            {
                var credential = GoogleCredential.FromFile(path);
                FirebaseApp.Create(new AppOptions { Credential = credential });
            }
            _messaging = FirebaseMessaging.DefaultInstance;
        }
        public async Task SendFirebaseNotificationAsync(string deviceToken, string title, string body)
        {
            var message = new Message()
            {
                Notification = new FirebaseAdmin.Messaging.Notification 
                {
                    Title = title,
                    Body = body
                },
                Token = deviceToken,
            };
            await _messaging.SendAsync(message);
        }
    }
}
