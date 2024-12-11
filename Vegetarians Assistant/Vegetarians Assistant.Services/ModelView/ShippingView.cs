namespace Vegetarians_Assistant.Repo.Entity;

public partial class ShippingView
{
    public int Id { get; set; }

    public int Status { get; set; }

    public string StatusText { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public string Message { get; set; } = null!;

    public DateTime PickDate { get; set; }

    public DateTime DeliverDate { get; set; }

    public string CustomerFullname { get; set; } = null!;

    public string CustomerTel { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int ShipMoney { get; set; }

    public int Insurance { get; set; }

    public int Value { get; set; }

    public int PickMoney { get; set; }

    public int? UserId { get; set; }

    public int? OrderId { get; set; }

    public long TrackingId { get; set; }
}
