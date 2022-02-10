using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Persistence.DataBase.Configuracion
{
    /// <summary>
    /// Clase de configuración y restricciones de las entidades de la capa Modelos.
    /// </summary>
    public class SaleConfiguracion
    {
        public SaleConfiguracion(EntityTypeBuilder<Sale> entityTypeBuilder)
        {
            // Clave primaria compuesta por dos propiedades.
            entityTypeBuilder.HasKey(x => new
            {
                x.Year,
                x.Month
            });
            
        }
    }
}
