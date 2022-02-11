using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Persistence.DataBase.Configuracion
{
    /// <summary>
    /// Clase de configuración y restricciones de las entidades de la capa Modelos.
    /// </summary>
    public class ProductExtraInformationConfiguracion
    {
        public ProductExtraInformationConfiguracion(EntityTypeBuilder<ProductExtraInformation> entityTypeBuilder)
        {
            entityTypeBuilder.Property(x => x.SKU).IsRequired().HasMaxLength(20);
            entityTypeBuilder.Property(x => x.Descripcion).IsRequired().HasMaxLength(500);           
        }
    }
}
