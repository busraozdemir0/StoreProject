using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;

namespace Repositories
{
    public sealed class ProductRepository : RepositoryBase<Product>, IProductRepository  // sealed => ProductRepository classının başka herhangi bir class tarafından kalıtım alınamayacağı anlamına gelir
    {
        public ProductRepository(RepositoryContext context) : base(context) // Veritabanına erişim için base contexti gönderdik
        {
        }

        public void CreateOneProduct(Product product)=>Create(product);

        public void DeleteOneProduct(Product product)=>Remove(product);

        public IQueryable<Product> GetAllProducts(bool trackChanges)=>FindAll(trackChanges); // base üzerindeki FindAll'u kullanabiliriz

        public IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters p)
        {
            return _context
                .Products
                .FilteredByCategoryId(p.CategoryId)     // parametreden gelen CategoryId'ye göre filtreleyecek
                .FilteredBySearchTerm(p.SearchTerm)     // parametreden gelen kelimeye göre arama işlemi
                .FilteredByPrice(p.MinPrice,p.MaxPrice,p.IsValidPrice)  // min ve max fiyatlar girilip filtreleme
                .ToPaginate(p.PageNumber,p.PageSize);  // sayfalama işlemi
        }

        // ? veritabanında null değer de olabileceği anlamına geliyor.
        public Product? GetOneProduct(int id, bool trackChanges) // bool trackChanges => ifadesi değişiklikleri izlemek için yazılmaktadır.
        {
            return FindByCondition(p=>p.ProductId.Equals(id),trackChanges);
        }

        public IQueryable<Product> GetShowcaseProducts(bool trackChanges)
        {
            return FindAll(trackChanges)
                    .Where(p=>p.ShowCase.Equals(true));
        }

        public void UpdateOneProduct(Product entity)=>Update(entity);
    }
}