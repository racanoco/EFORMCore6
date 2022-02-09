using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Persistence.DataBase.Configuracion
{
    /// <summary>
    /// Clase de configuración y restricciones de las entidades de la capa Modelos.
    /// </summary>
    public class OrderDetailConfiguracion
    {
        public OrderDetailConfiguracion(EntityTypeBuilder<OrderDetail> entityTypeBuilder)
        {            
            entityTypeBuilder.Property(x => x.OrderId).HasMaxLength(20);
        }
    }
}
