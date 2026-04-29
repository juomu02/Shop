using System.Net;

namespace Shop.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public enum IsPaid 
        { 
            New = 0, 
            Paid = 1, 
            NotPaid = 2
            }
        public IsPaid Status {get; set;}
        public ICollection<ProductInOrder> ProductInOrders { get; set; }
    }
}