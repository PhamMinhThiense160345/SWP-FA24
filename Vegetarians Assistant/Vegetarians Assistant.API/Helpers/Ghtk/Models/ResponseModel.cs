namespace Vegetarians_Assistant.API.Helpers.Ghtk.Models;

public record ResponseModel(bool Success, string? Message);
public record ResponseModel<T>(bool Success, string? Message, T? Content) : ResponseModel(Success, Message);


