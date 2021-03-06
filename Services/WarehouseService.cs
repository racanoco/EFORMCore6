using Microsoft.EntityFrameworkCore;
using Models;
using Persistence.DataBase;
using Services.ComplexModels;

namespace Services
{
    public class WarehouseService
    {

        #region VARIABLES GLOBALES
        private readonly ApplicationDbContext _context;
        #endregion

        #region CONSTRUCTOR
        public WarehouseService(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region GET
        public async Task<IEnumerable<Warehouse>> GetAll()
        {
            var warehouse = await _context.Warehouses
                .Include(x => x.WarehouseProductList)
                .ThenInclude(x => x.Product)                
                .ToListAsync();

            return warehouse;
        }

        public IEnumerable<WarehouseProductReportDTO> GetAllWithProducts()
        {
            return (
                from w in _context.Warehouses
                from wp in _context.WarehouseProduct.Where(x => x.WarehousedId == w.WarehouseId && x.Product.Price >= 1000)
                select new WarehouseProductReportDTO
                {
                    WarehouseName = w.Name,
                    ProductName = wp.Product.Name,
                    ProductPrice = wp.Product.Price
                }
            ).ToList();
        }

        // Memory option
        //public IEnumerable<WarehouseProductReport> GetAllWithProducts()
        //{
        //    var warehouses = this.GetAll();
        //    var warehouseProducts = warehouses.SelectMany(x => x.Products).ToList();

        //    return (
        //        from w in warehouses
        //        from wp in warehouseProducts.Where(x => x.WareHouseId == w.WarehouseId)
        //        select new WarehouseProductReport 
        //        {
        //            WarehouseName = w.Name,
        //            ProductName = wp.Product.Name,
        //            ProductPrice = wp.Product.Price
        //        }
        //    ).ToList();
        //}
        #endregion

        #region DELETE
        //public void RemoveBulkSoftDelete()
        //{
        //    _context.Warehouses
        //            .Where(x => x.WarehouseId >= 80)
        //            .Update(x => new Warehouse
        //            {
        //                IsDeleted = true
        //            });
        //}

        //public void RemoveBulk()
        //{
        //    _context.Warehouses
        //            .Where(x => x.WarehouseId >= 80)
        //            .Delete();
        //}

        public void Remove(List<int> ids)
        {
            var warehouses = ids.Select(x => new Warehouse
            {
                WarehouseId = x
            }).ToList();

            _context.RemoveRange(warehouses);

            _context.SaveChanges();
        }

        //public void Remove(int id)
        //{
        //    var originalEntry = _context.Warehouses.Single(x => x.WarehouseId == id);
        //    originalEntry.IsDeleted = true;

        //    _context.Update(originalEntry);
        //    _context.SaveChanges();
        //}

        #endregion
    }
}
