namespace Shop.Entities
{
    public class ProductInBasket
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Count { get; set; }
        public Basket Basket { get; set; }
    }
}