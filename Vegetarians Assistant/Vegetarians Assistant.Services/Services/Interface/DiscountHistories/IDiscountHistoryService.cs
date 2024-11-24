using Vegetarians_Assistant.Repo.Entity;

namespace Vegetarians_Assistant.Services.Services.Interface.DiscountHistories;
public interface IDiscountHistoryService
{
    Task<(bool, string)> AddAsync(DiscountHistory discountHistory);

    Task<(bool, string)> UpdateStatusAsync(int userId, int tierId, string status);
}
