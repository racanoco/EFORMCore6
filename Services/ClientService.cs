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

        /// <summary>
        /// Utilizar el Single siempre que estemos seguro de que existe el registro.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<Client> GetById_Single(int clientId)
        {
            Client client = new();

            if (_context.Clients.Any(x => x.ClientId == clientId))
            {
                client = await _context.Clients.SingleAsync(x => x.ClientId == clientId);
            }

            return client;
        }

        /// <summary>
        /// Utilizar el SingleOrDefault siempre que no estemos seguro de que existe el registro.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<Client?> GetById_SingleOrDefault(int clientId)
        {
            try
            {
                return await _context.Clients.SingleOrDefaultAsync(x => x.ClientId == clientId);               
            }
            catch (Exception)
            {
                throw;
            }

            
        }
        public async Task<Client> GetFirstClient()
        {
            var client = await _context.Clients.FirstAsync();

            return client;
        }
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
