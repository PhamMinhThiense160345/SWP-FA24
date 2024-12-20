using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.Services.Interface.IFirebase
{
    public interface IFirebaseService
    {
        Task SendFirebaseNotificationAsync(string deviceToken, string title, string body);
    }
}
