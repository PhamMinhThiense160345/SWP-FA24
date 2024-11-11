namespace Vegetarians_Assistant.Services.ModelView;
public class AddPaymentView
{
    public int? OrderId { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentStatus { get; set; }

    public string? TransactionId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? Amount { get; set; }

    public decimal? RefundAmount { get; set; }

    public string? ReturnUrl { get; set; }

    public string? CancelUrl { get; set; }

}


