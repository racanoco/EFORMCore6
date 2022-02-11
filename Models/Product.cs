namespace Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public List<WarehouseProduct> WarehouseProductList { get; set;}
        public ProductExtraInformation ExtraInformation { get; set; }
    }
}
