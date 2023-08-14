using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext context)
            : base(context) { }

        public IQueryable<Order> Orders =>
            _context.Orders
                .Include(o => o.Lines)
                .ThenInclude(cl => cl.Product) // sorgulama yaparken Order, Line ve Product'ları bir araya getirmesini sağlar
                .OrderBy(o => o.Shipped)
                .ThenByDescending(o => o.OrderId);

        public int NumberOfInProcess => _context.Orders.Count(o => o.Shipped.Equals(false)); // gönderilmemiş olan siparişlerin sayısını verir

        public void Complete(int id)
        {
            var order = FindByCondition(o => o.OrderId.Equals(id), true);
            if (order is null)
                throw new Exception("Order could not found");
            order.Shipped=true;  // eğer sipariş varsa kargoya verildi alanını true yap
           
        }

        public Order? GetOneOrder(int id)
        {
            return FindByCondition(o=>o.OrderId.Equals(id), false);
        }

        public void SaveOrder(Order order) // sipariş kaydı
        {
            _context.AttachRange(order.Lines.Select(l=>l.Product));
            if(order.OrderId==0)
                _context.Orders.Add(order); // sipariş order tablosuna eklenerek kaydedildi
            _context.SaveChanges();
        }
    }
}
