using Entities.Models;

namespace Repositories.Extensions
{
    public static class ProductRepositoryExtension
    {
        public static IQueryable<Product> FilteredByCategoryId(this IQueryable<Product> products,int? categoryId )
        {
            if(categoryId is null) // categoryId boşsa tüm ürünler listelensin
                return products;  
            else  // categoryId dolu gelmişse gelen id'ye göre ürünler filtrelensin
                return products.Where(prd=>prd.CategoryId.Equals(categoryId)); 

        }
    }
}