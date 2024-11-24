using Vegetarians_Assistant.Repo.Entity;

namespace Vegetarians_Assistant.Services.ModelView;
public class DiscountHistoryView
{
    public int? UserId { get; set; }

    public int? TierId { get; set; }

    public DateTime? GrantedDate { get; set; }

    public decimal? DiscountRate { get; set; }

    public string? Status { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public DiscountHistory MapToDiscountHistory() => new()
    {
        UserId = UserId,
        TierId = TierId,
        GrantedDate = GrantedDate,
        DiscountRate = DiscountRate,
        Status = Status,
        ExpirationDate = ExpirationDate,
    };
}
