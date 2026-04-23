namespace Shop.Entities
{
    public class Basket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ICollection<ProductInBasket> ProductInBaskets { get; set;} 
    }
}