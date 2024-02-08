namespace Share.Entities;

public class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int OrderDetailsId { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime OrderDate { get; set; }

    public virtual OrderDetail OrderDetails { get; set; } = null!;
}
