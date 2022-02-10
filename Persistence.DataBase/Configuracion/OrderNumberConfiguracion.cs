using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Persistence.DataBase.Configuracion
{
    /// <summary>
    /// Clase de configuración y restricciones de las entidades de la capa Modelos.
    /// </summary>
    public class OrderNumberConfiguracion
    {
        public OrderNumberConfiguracion(EntityTypeBuilder<OrderNumber> entityTypeBuilder)
        {
            // Especificamos cual es la primary key del modelo.
            entityTypeBuilder.HasKey(x => x.Year);
            entityTypeBuilder.HasData(new OrderNumber
            {
                Year = DateTime.Now.Year,
                Value = 0
            });
            
        }
    }
}
