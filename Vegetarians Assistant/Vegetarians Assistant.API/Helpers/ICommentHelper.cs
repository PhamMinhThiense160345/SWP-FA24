using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.API.Helpers;

public interface ICommentHelper
{
    ResponseView CheckContent(string content);
}
