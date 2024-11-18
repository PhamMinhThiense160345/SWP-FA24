using Net.payOS.Types;
using System.ComponentModel.DataAnnotations;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.API.Requests;

public class CheckoutRequest
{
    [Required]
    public int OrderId { get; set; }

    [Required]
    public GoogleMapLocationView ShopLocation { get; set; } = null!;

    [Required]
    public GoogleMapLocationView CustomerLocation { get; set; } = null!;

    [Required]
    public decimal ShippingFeeUnit { get; set; } = 5000;

    [Required]
    public string DecryptionKey { get; set; } = null!;
}
