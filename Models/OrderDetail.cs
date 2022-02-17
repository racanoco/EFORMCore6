using Models.Helpers;

namespace Models
{
    public class OrderDetail : Audit
    {
        public Guid OrderDetailId { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }    
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal UnitePrice { get; set; }
        public int Quantity { get; set; }
        public decimal Iva { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
