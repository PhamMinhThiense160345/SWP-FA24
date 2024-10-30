using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.Feedback
{
    public interface IFeedbackManagementService
    {
        Task<List<FeedbackView>> GetAllFeedback();
        Task<FeedbackView?> GetFeedbackByFeedbackId(int id);
    }
}
