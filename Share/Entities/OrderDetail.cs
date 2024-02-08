namespace Share.Entities;

public class OrderDetail
{
    public int Id { get; set; }

    public string ProductId { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
    //En en till många relation...
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
