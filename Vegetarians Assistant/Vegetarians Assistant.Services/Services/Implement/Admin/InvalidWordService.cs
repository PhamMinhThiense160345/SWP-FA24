using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.Services.Interface.Admin;

namespace Vegetarians_Assistant.Services.Services.Implement.Admin;
public class InvalidWordService(IUnitOfWork unitOfWork) : IInvalidWordService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<(bool, string)> AddAsync(string content)
    {
        try
        {
            var exist = await _unitOfWork.InvalidWordRepository.GetAsync(x => x.Content.ToLower().Trim() == content.ToLower().Trim());
            if (exist.Any()) throw new Exception("Word is already exist");

            await _unitOfWork.InvalidWordRepository.InsertAsync(new InvalidWord() { Content = content });
            await _unitOfWork.SaveAsync();

            return (true, "Add new invalid word successful");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<bool> IsValidAsync(string content)
    {
        try
        {
            var exist = await _unitOfWork.InvalidWordRepository.GetAsync(x => x.Content.ToLower().Trim() == content.ToLower().Trim());
            if (exist.Any()) throw new Exception();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
