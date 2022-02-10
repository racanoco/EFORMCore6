
namespace Models
{
    public class Order
    {       
        public string OrderId { get; set; } 
        public int ClientId { get; set; } 
        public Client Client { get; set; }  // Relación de muchos a uno.
        public List<OrderDetail> ItemsOrderDetail { get; set; }
        public decimal Iva { get; set; }
        public decimal SubTotal { get; set; }   
        public decimal Total { get; set; }
    }
}
