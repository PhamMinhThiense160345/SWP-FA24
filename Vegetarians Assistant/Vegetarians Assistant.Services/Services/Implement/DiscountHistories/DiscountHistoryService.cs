using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.Services.Interface.DiscountHistories;

namespace Vegetarians_Assistant.Services.Services.Implement.DiscountHistories;
public class DiscountHistoryService(IUnitOfWork unitOfWork) : IDiscountHistoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<(bool, string)> AddAsync(DiscountHistory discountHistory)
    {
        try
        {
            var exist = await _unitOfWork.DiscountHistoryRepository.GetAsync(
                x => x.UserId == discountHistory.UserId && x.TierId == discountHistory.TierId);
            if (exist.Any()) throw new Exception("Discount history is already exist");

            if(discountHistory.ExpirationDate <= discountHistory.GrantedDate)
                throw new Exception("Expiration date must be after granted date");

            await _unitOfWork.DiscountHistoryRepository.InsertAsync(discountHistory);
            await _unitOfWork.SaveAsync();
            return (true, "Add new discount history successful");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool, string)> UpdateStatusAsync(int userId, int tierId, string status)
    {
        try
        {
            var exist = await _unitOfWork.DiscountHistoryRepository.GetAsync(
                x => x.UserId == userId && x.TierId == tierId);
            if (!exist.Any()) throw new Exception("Discount history is not exist");
            var discount = exist.FirstOrDefault();
            discount!.Status = status;

            await _unitOfWork.DiscountHistoryRepository.UpdateAsync(discount);
            await _unitOfWork.SaveAsync();
            return (true, "Update discount status successful");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }
}
