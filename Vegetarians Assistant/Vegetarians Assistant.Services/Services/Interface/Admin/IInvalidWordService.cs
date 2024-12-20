using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.Admin;
public interface IInvalidWordService
{
    Task<bool> IsValidAsync(string content);
    Task<IEnumerable<InvalidWord>> GetAllAsync();
    Task<ResponseView> AddAsync(string content);
    Task<ResponseView> RemoveAsync(string content);
    Task<ResponseView> UpdateAsync(string content, string newContent);
}
