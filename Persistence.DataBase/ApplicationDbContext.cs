using Common;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence.DataBase.Configuracion;

namespace Persistence.DataBase
{
    public class ApplicationDbContext: DbContext
    {
        #region COSTRUCTOR

        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {}
        #endregion

        #region TABLES
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderNumber> OrderNumbers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Parameter.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Models configuration or contraints.
            _ = new ClientConfiguracion(builder.Entity<Client>());
            _ = new OrderConfiguracion(builder.Entity<Order>());
            _ = new OrderDetailConfiguracion(builder.Entity<OrderDetail>());
            _ = new ProductConfiguracion(builder.Entity<Product>());
            _ = new OrderNumberConfiguracion(builder.Entity<OrderNumber>());
            _ = new SaleConfiguracion(builder.Entity<Sale>());
            _ = new ContryConfiguracion(builder.Entity<Country>());
            _ = new WarehouseConfiguracion(builder.Entity<Warehouse>());
        }

    }
}
