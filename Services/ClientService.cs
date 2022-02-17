using Microsoft.EntityFrameworkCore;
using Models;
using Persistence.DataBase;
using Services.QueryHelpers;

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

        public async Task<IEnumerable<Client>> GetAllAndCountry()
        {
            var clients = await _context.Clients
                .Include(x => x.Country)
                .OrderByDescending(x => x.ClientId)
                .ToListAsync();

            return clients;
        }


        public Client Get_GetBaseQuery(int id)
        {
            return _context.Clients.GetBaseQuery().SingleOrDefault(x => x.ClientId == id);
        }

        public Client GetFirstClient_GetBaseQuery()
        {
            return _context.Clients.GetBaseQuery().FirstOrDefault();
        }
        public IEnumerable<Client> GetAll_GetBaseQuery()
        {
            return _context.Clients
                           .GetBaseQuery()
                           .OrderByDescending(x => x.ClientId)
                           .ToList();
        }
        #endregion

        #region POST
        /// <summary>
        /// Guarda una lista de un modelo
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        public void Create(List<Client> clients)
        {
             _context.AddRange(clients); // AddRange se utiliza para guardar una lista.
             _context.SaveChanges();
        }

        /// <summary>
        /// Guarda un modelo.
        /// </summary>
        /// <param name="client"></param>
        public void Create(Client client)
        {
            _context.Add(client);
            _context.SaveChanges();
        }
        #endregion

        #region PUT

        /// <summary>
        /// Guarda los datos de un modelo.
        /// </summary>
        /// <param name="client"></param>
        public void Update(Client client)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            // Obtenemos el registro de la bade de datos.
            var originalEntry = _context.Clients.Single(x => x.ClientId == client.ClientId);

            // Y Solo agregamos el valor que queremos actualizar.
            originalEntry.Name = client.Name;

            _context.SaveChanges();
        }

        /// <summary>
        /// Guarda una lista de un modelo
        /// </summary>
        /// <param name="clients"></param>
        public void Update(List<Client> clients)
        {
            // Obtenemos los ids de la lista.
            var ids = clients.Select(x => x.ClientId);

            // Obtenemos todos los registros de los ids filtrados, No utilizar container si hay muchos Ids ya que se demora la select
            var entries = _context.Clients.Where(x => ids.Contains(x.ClientId)).ToList();

            // Actualizamos los registros.
            foreach (var entry in entries)
            {
                var client = clients.Single(x => x.ClientId == entry.ClientId);
                entry.Name = client.Name;
            }

            _context.SaveChanges();
        }

        
        #endregion
    }
}
