using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Persistence.DataBase.Configuracion
{
    /// <summary>
    /// Clase de configuración y restricciones de las entidades de la capa Modelos.
    /// </summary>
    public class WarehouseConfiguracion
    {
        public WarehouseConfiguracion(EntityTypeBuilder<Warehouse> entityTypeBuilder)
        {            
            entityTypeBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);            

            // Código para insertar datos de pruebas.
            entityTypeBuilder.HasData(
                new Warehouse
                {
                    WarehouseId = 1,                    
                    Name = "Almacen Zona A"
                },
                new Warehouse
                {
                    WarehouseId = 2,                    
                    Name = "Almacen Zona B"
                });
        }
    }
}
