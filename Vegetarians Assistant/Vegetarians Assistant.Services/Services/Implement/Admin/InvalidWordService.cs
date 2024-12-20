using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Admin;

namespace Vegetarians_Assistant.Services.Services.Implement.Admin;
public class InvalidWordService(IUnitOfWork unitOfWork) : IInvalidWordService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

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

    public async Task<IEnumerable<InvalidWord>> GetAllAsync()
        => await _unitOfWork.InvalidWordRepository.GetAllAsync();

    public async Task<ResponseView> AddAsync(string content)
    {
        try
        {
            var exist = await _unitOfWork.InvalidWordRepository.GetAsync(x => x.Content.ToLower().Trim() == content.ToLower().Trim());
            if (exist.Any()) throw new Exception("Từ đã tồn tại trong hệ thống");

            await _unitOfWork.InvalidWordRepository.InsertAsync(new InvalidWord() { Content = content });
            await _unitOfWork.SaveAsync();

            return new(true, "Thêm mới từ thành công");
        }
        catch (Exception ex)
        {
            return new(false, ex.Message);
        }
    }

    public async Task<ResponseView> UpdateAsync(string content, string newContent)
    {
        try
        {
            var words = await _unitOfWork.InvalidWordRepository
                .FindAsync(x => x.Content.Trim().ToLower() == content.Trim().ToLower());

            if (!words.Any()) throw new Exception("Không tìm thấy từ cần cập nhật");

            foreach (var word in words)
            {
                word.Content = newContent;
                await _unitOfWork.InvalidWordRepository.UpdateAsync(word);
            }
            return new(true, "Cập nhật từ từ thành công");
        }
        catch (Exception ex)
        {
            return new(false, ex.Message);
        }
    }

    public async Task<ResponseView> RemoveAsync(string content)
    {
        try
        {
            var words = await _unitOfWork.InvalidWordRepository
                .FindAsync(x => x.Content.Trim().ToLower() == content.Trim().ToLower());

            if (!words.Any()) throw new Exception("Không tìm thấy từ cần xóa");

            foreach (var word in words)
            {
                await _unitOfWork.InvalidWordRepository.DeleteAsync(word);
            }
            return new(true, "Xóa từ thành công");
        }
        catch (Exception ex)
        {
            return new(false, ex.Message);
        }
    }
}
