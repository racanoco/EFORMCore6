using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Persistence.DataBase.Configuracion
{
    /// <summary>
    /// Clase de configuración y restricciones de las entidades de la capa Modelos.
    /// </summary>
    public class ProductConfiguracion
    {
        public ProductConfiguracion(EntityTypeBuilder<Product> entityTypeBuilder)
        {            
            entityTypeBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            // Código para insertar datos de pruebas.
            entityTypeBuilder.HasData(
                new Product
                {
                    ProductId = 1,                    
                    Name = "Producto 1",
                    Price = 600
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Producto 2",
                    Price = 700
                });
        }
    }
}
