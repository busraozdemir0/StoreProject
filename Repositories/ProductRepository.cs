using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext context) : base(context) // Veritabanına erişim için base contexti gönderdik
        {
        }

        public void CreateOneProduct(Product product)=>Create(product);

        public void DeleteOneProduct(Product product)=>Remove(product);

        public IQueryable<Product> GetAllProducts(bool trackChanges)=>FindAll(trackChanges); // base üzerindeki FindAll'u kullanabiliriz

        // ? veritabanında null değer de olabileceği anlamına geliyor.
        public Product? GetOneProduct(int id, bool trackChanges) // bool trackChanges => ifadesi değişiklikleri izlemek için yazılmaktadır.
        {
            return FindByCondition(p=>p.ProductId.Equals(id),trackChanges);
        }

        public void UpdateOneProduct(Product entity)=>Update(entity);
    }
}