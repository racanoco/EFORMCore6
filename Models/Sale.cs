namespace Models
{
    public class Sale
    {
        public int Year { get; set; }
        public int Month { get; set; } 
        public decimal Iva { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
