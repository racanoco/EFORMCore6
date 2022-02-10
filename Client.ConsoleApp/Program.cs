using Common;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBase;

namespace Client.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer(Parameter.ConnectionString);

            var dbcontext = new ApplicationDbContext(optionBuilder.Options);
            TestConnection(dbcontext);
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
    }
}
