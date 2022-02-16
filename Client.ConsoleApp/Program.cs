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
            var productService = new ProductService(context);
            var warehouseService = new WarehouseService(context);

            using (context)
            {
                PrintClient(clientService);
                PrintClientById_Single(clientService, 2);
                PrintClientById_SingleOrDefault(clientService, 3);
                PrintClientAndCountry(clientService);
                ProductsExistsByName(productService, "Producto 2");
                PrintWarehousesAndProducts(warehouseService);
                PrintProductsByPagingTable(productService);
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

        #region CLIENT      
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

        static void PrintClientAndCountry(ClientService clientService)
        {
            try
            {
                var resultadoClients = clientService.GetAllAndCountry();

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

        static void PrintClientById_Single(ClientService clientService, int id)
        {
            var resultadoClientById = clientService.GetById_Single(id);

            var table = new Table("ClientId", "NIF", "Name", "Country");
            
            table.AddRow(resultadoClientById.Result.ClientId, resultadoClientById.Result.NIF, resultadoClientById.Result.Name, resultadoClientById.Result.Country?.Name ?? "-");
            
            Console.Write(table.ToString());
           
        }

        static void PrintClientById_SingleOrDefault(ClientService clientService, int id)
        {
            var resultadoClientById = clientService.GetById_SingleOrDefault(id);

            var table = new Table("ClientId", "NIF", "Name", "Country");

            if (resultadoClientById.Result is not null)
            {
                table.AddRow(resultadoClientById.Result.ClientId, resultadoClientById.Result.NIF, resultadoClientById.Result.Name, resultadoClientById.Result.Country?.Name ?? "-");

                Console.Write(table.ToString());
            }

            Console.Write("Valor NULL en el objeto Client");
        }
        #endregion

        #region PRODUCT
        static void ProductsExistsByName(ProductService productService, string name)
        {
            var resultadoExistsByName = productService.ExistsByName(name);
            Console.Write(resultadoExistsByName.Result ? "Product exists" : "Product not exists");
        }

        static void PrintWarehousesAndProducts(WarehouseService warehouseService)
        {
            var resultadoWarehouses = warehouseService.GetAll();
            var table = new Table("Warehouse", "Product", "Price");

            foreach (var warehouse in resultadoWarehouses.Result)
            {
                table.AddRow(warehouse.Name);

                foreach (var warehouseProduct in warehouse.WarehouseProductList)
                {
                    table.AddRow("", warehouseProduct.Product.Name, warehouseProduct.Product.Price);
                }
            }

            Console.Write(table.ToString());
        }

        static void PrintProductsByPagingTable(ProductService productService)
        {
            var page = 0;

            do
            {
                var table = new Table("ProductId", "Name", "Price");
                var products = productService.GetPaged(page, 2);

                if (!products.Result.Any())
                {
                    Console.WriteLine("No hay más registros que visualizar ...");
                    break;
                }

                foreach (var product in products.Result)
                {
                    table.AddRow(product.ProductId, product.Name, product.Price);
                }

                Console.Write(table.ToString());
                Console.WriteLine("Presione enter para seguir buscando");
                Console.ReadLine();

                Console.Clear();

                page++;
            } while (true);
        }
        #endregion
    }
}
