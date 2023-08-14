using Entities.Models;

namespace Repositories.Contracts
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders{get;}
        Order? GetOneOrder(int id);
        void Complete(int id); // sipariş tamamlama
        void SaveOrder(Order order); // sipariş kaydetme
        int NumberOfInProcess{get;}
    }
}