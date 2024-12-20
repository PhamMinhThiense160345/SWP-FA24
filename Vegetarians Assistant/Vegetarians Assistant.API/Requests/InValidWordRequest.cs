namespace Vegetarians_Assistant.API.Requests;
public record InValidWordRequest(string Content);
public record UpdateInvalidWordRequest(string Content, string NewContent);