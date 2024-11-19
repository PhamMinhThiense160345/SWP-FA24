namespace Vegetarians_Assistant.API.Requests;

public record NotificationRequest(string Token, string Title, string Body);