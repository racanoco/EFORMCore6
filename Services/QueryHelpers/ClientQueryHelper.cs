using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.QueryHelpers
{
    public static class ClientQueryHelper
    {
        public static IQueryable<Client> GetBaseQuery(this DbSet<Client> query)
        {
            return query.Include(x => x.Country)
                        .AsQueryable();
        }
    }
}
