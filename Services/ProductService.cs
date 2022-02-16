using Microsoft.EntityFrameworkCore;
using Models;
using Persistence.DataBase;

namespace Services
{
    public class ProductService
    {

        #region VARIABLES GLOBALES
        private readonly ApplicationDbContext _context;
        #endregion

        #region CONSTRUCTOR
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region GET                
        public async Task<IEnumerable<Product>> GetAll()
        {
            var productos = await _context.Products
                .Where(x => x.Price == 500)
                .ToListAsync();

            var ids = new List<int> { 1, 2 };

            productos = await _context.Products
                .Where(x => ids.Contains(x.ProductId)) // Equivale al IN de sql server
                .ToListAsync();

            productos = await _context.Products
               .Where(x => !ids.Contains(x.ProductId)) // Equivale al IN de sql server
               .ToListAsync();

            productos = await _context.Products
              .Where(x => x.Name.Contains("Producto 1")) // Equivale al IN de sql server
              .ToListAsync();

            productos = await _context.Products
              .Where(x => x.Name.EndsWith("1")) // Termine en 1
              .ToListAsync();

            productos = await _context.Products
              .Where(x => x.Name.StartsWith("P")) // Empieza en P
              .ToListAsync();


            return productos;
        }

        public async Task<IEnumerable<Product>> GetAll(bool filterBy = false)
        {
            var productos = await _context.Products
                .Where(x => !filterBy || x.Name.StartsWith("P"))
                .ToListAsync();          


            return productos;
        }

        public Task<bool> ExistsByName(string name)
        {
            // Equivale al where exists() de sql
            return Task.FromResult(_context.Products.Any(x => x.Name == name));            
        }

        public async Task<IEnumerable<Product>> GetPaged(int page, int take)
        {
            var products = await _context.Products
                           .OrderBy(x => x.Name)
                           .Skip(page * take)
                           .Take(take)
                           .ToListAsync();

            return products;
        }
        #endregion
    }
}
