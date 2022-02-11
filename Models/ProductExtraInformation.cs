namespace Models
{
    public class ProductExtraInformation
    {
        public int ProductExtraInformationID { get; set; }
        public string SKU { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Size { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
