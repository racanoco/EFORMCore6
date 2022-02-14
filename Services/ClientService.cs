using Microsoft.EntityFrameworkCore;
using Models;
using Persistence.DataBase;

namespace Services
{
    public class ClientService
    {

        #region VARIABLES GLOBALES
        private readonly ApplicationDbContext _context;
        #endregion

        #region CONSTRUCTOR
        public ClientService(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region GET
        public async Task<IEnumerable<Client>> GetAll()
        {
            var clients = await _context.Clients
                .OrderByDescending(x => x.ClientId)
                .ToListAsync();

            return clients;
        }
        #endregion
    }
}
