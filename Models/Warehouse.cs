namespace Models
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string Name { get;set; }
        public List<WarehouseProduct> WarehouseProductList { get; set; }
    }
}
