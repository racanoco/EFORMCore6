
namespace Models
{
    public class Order
    {       
        public string OrderId { get; set; } 
        public int ClientId { get; set; }      
        public decimal Iva { get; set; }
        public decimal SubTotal { get; set; }   
        public decimal Total { get; set; }
    }
}
