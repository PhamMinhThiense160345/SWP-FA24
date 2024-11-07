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
        Task<List<FeedbackInfoView>> GetAllFeedback();
        Task<List<FeedbackInfoView?>> GetFeedbackByDishId(int id);
        Task<bool> CreateFeedback(FeedbackView newFeedback);
    }
}
