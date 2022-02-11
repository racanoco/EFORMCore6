using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Persistence.DataBase.Configuracion
{
    /// <summary>
    /// Clase de configuración y restricciones de las entidades de la capa Modelos.
    /// </summary>
    public class ContryConfiguracion
    {
        public ContryConfiguracion(EntityTypeBuilder<Country> entityTypeBuilder)
        {
            entityTypeBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            entityTypeBuilder.HasData(
                new Country
                {
                    CountryId = 1,
                    Name = "España"
                },
                new Country
                {
                    CountryId = 2,
                    Name = "Catalunya"
                });
        }
    }
}
