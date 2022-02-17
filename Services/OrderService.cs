using Microsoft.EntityFrameworkCore;
using Models;
using Persistence.DataBase;

namespace Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;
        private const decimal Iva = 0.18m;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Order> GetAll()
        {
            return _context.Orders
                           .Include(x => x.Client)
                            .ThenInclude(x => x.Country)
                           .ToList();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders
                           .Include(x => x.Client)
                            .ThenInclude(x => x.Country)
                           .ToListAsync();
        }

        public void Create(Order order)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                // 01. Obtener el correlativo de la orden 2019-0001
                PrepareOrderNumber(order);

                // 02. Completar los campos pendientes
                PrepareDetail(order);
                PrepareOrderAmounts(order);

                order.RegisteredAt = DateTime.Now;

                // 03. Crear la orden
                _context.Add(order);
                _context.SaveChanges();

                // 04. Calcula los montos por mes
                CalculateAmounts();

                // Confirmar cambios
                trans.Commit();
            }
        }

        private void CalculateAmounts()
        {
            var now = DateTime.Now;

            var entry = _context.Sales.SingleOrDefault(x => x.Year == now.Year && x.Month == now.Month);

            if (entry == null)
            {
                entry = new Sale
                {
                    Year = now.Year,
                    Month = now.Month
                };

                // Necesitamos crear el registro previamente
                _context.Sales.Add(entry);
                _context.SaveChanges();
            }

            // Creamos un query base
            var orders = _context.Orders.Where(x =>
                x.RegisteredAt.Year == now.Year
                && x.RegisteredAt.Month == now.Month
            );

            entry.Iva = orders.Sum(x => x.Iva);
            entry.SubTotal = orders.Sum(x => x.SubTotal);
            entry.Total = orders.Sum(x => x.Total);

            _context.SaveChanges();
        }

        private void PrepareOrderAmounts(Order order)
        {
            order.SubTotal = order.ItemsOrderDetail.Sum(x => x.SubTotal);
            order.Iva = order.ItemsOrderDetail.Sum(x => x.Iva);
            order.Total = order.ItemsOrderDetail.Sum(x => x.Total);
        }

        private void PrepareDetail(Order order)
        {
            foreach (var detail in order.ItemsOrderDetail)
            {
                detail.Total = Math.Round(detail.Quantity * detail.UnitePrice, 2);
                detail.SubTotal = Math.Round(detail.Total / (1 + Iva), 2);
                detail.Iva = Math.Round(detail.Total - detail.SubTotal, 2);
            }
        }

        private void PrepareOrderNumber(Order order)
        {
            var orderNumber = _context.OrderNumbers.Single(x => x.Year == DateTime.Now.Year);
            orderNumber.Value++;

            order.OrderId = orderNumber.Year.ToString() + "-" + orderNumber.Value.ToString().PadLeft(5, '0');
        }
    }
}
