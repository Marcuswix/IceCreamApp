namespace Share.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string ProductArticleNumber { get; set; } = null!;
        public int Quantity {  get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Totalprice { get; set; }
        public DateTime DateTime { get; set; }
    }
}
