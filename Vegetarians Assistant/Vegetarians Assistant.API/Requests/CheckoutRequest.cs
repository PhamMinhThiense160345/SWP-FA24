using Net.payOS.Types;
using System.ComponentModel.DataAnnotations;

namespace Vegetarians_Assistant.API.Requests;

public class CheckoutRequest
{
    [Required]
    public int OrderId { get; set; }

    [Required]
    public string DecryptionKey { get; set; } = null!;
}
