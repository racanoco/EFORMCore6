using BetterConsoleTables;
using Common;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBase;
using Services;

namespace Client.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer(Parameter.ConnectionString);

            var context = new ApplicationDbContext(optionBuilder.Options);

            TestConnection(context);

            var clientService = new ClientService(context);

            using (context)
            {
                PrintClient(clientService);
            }

            Console.Read();
            
        }

        static void TestConnection(ApplicationDbContext context)
        {
            try
            {
                // EnsureCreated: Método que garantiza qie existe la base de datos, si no existe, crea la base de datos para el contexto que se le pasa, si existe no hace nada.
                bool isConnection = context.Database.EnsureCreated();

                if (isConnection)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Connection successful");
                    Console.Read();
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Connection unsuccessful");
                Console.Read();               
            }
            
        }

        static void PrintClient(ClientService clientService)
        {
            try
            {
                var resultadoClients = clientService.GetAll();

                var table = new Table("ClientId", "NIF", "Name", "Country");

                foreach (var client in resultadoClients.Result)
                {
                    table.AddRow(client.ClientId, client.NIF, client.Name, client.Country?.Name ?? "-");
                }

                Console.Write(table.ToString());
            }
            catch (Exception)
            {
                
            }

        }
    }
}
