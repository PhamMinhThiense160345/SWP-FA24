using Vegetarians_Assistant.Repo.Entity;

namespace Vegetarians_Assistant.Services.Services.Interface.Admin;
public interface IInvalidWordService 
{
    Task<bool> IsValidAsync(string content);

    Task<(bool, string)> AddAsync(string content);
}
