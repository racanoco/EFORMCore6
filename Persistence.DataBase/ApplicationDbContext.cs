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
            new ClientConfiguracion(builder.Entity<Client>());
            new OrderConfiguracion(builder.Entity<Order>());
            new OrderDetailConfiguracion(builder.Entity<OrderDetail>());
            new ProductConfiguracion(builder.Entity<Product>());
        }

    }
}
