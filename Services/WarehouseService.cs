using Microsoft.EntityFrameworkCore;
using Models;
using Persistence.DataBase;

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
        #endregion
    }
}
