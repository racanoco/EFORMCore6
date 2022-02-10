using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Persistence.DataBase.Configuracion
{
    /// <summary>
    /// Clase de configuración y restricciones de las entidades de la capa Modelos.
    /// </summary>
    public class ClientConfiguracion
    {
        public ClientConfiguracion(EntityTypeBuilder<Client> entityTypeBuilder)
        {
            entityTypeBuilder.Property(x => x.NIF).IsRequired().HasMaxLength(30);
            entityTypeBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            // Código para insertar datos de pruebas.
            entityTypeBuilder.HasData(
                new Client
                {
                    ClientId = 1,
                    NIF = "52169902E",
                    Name = "Emma Cano Martos"
                },
                new Client
                {
                    ClientId = 2,
                    NIF = "52169902B",
                    Name = "Bueno Cano Martos"
                });
        }
    }
}
