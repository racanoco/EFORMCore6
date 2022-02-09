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
        }
    }
}
