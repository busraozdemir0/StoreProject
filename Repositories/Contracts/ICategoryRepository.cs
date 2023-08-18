using Entities.Models;

namespace Repositories.Contracts
{
    public interface ICategoryRepository:IRepositoryBase<Category>
    {
        void CreateOneCategory(Category category);
        void DeleteOneCategory(Category category);
        void UpdateOneCategory(Category entity);
        Category? GetOneCategory(int id,bool trackChanges);
    }
}