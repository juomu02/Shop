namespace Shop.Entities
{
    public class ProductInOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Count { get; set; }
        public Order Order { get; set; }
    }
}