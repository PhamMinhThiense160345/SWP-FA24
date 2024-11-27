using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class ShippingView
{
    public int ShippingId { get; set; }

    public int OrderId { get; set; }

    public string? ShipperName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? TransportCompany { get; set; }

    public DateTime? PickupTime { get; set; }

    public DateTime? DeliveryTime { get; set; }

    public string? FailureReason { get; set; }

}
