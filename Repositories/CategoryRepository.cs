using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneCategory(Category category)=>Create(category);

        public void DeleteOneCategory(Category category)=>Remove(category);

        public Category? GetOneCategory(int id, bool trackChanges)
        {
            return FindByCondition(c=>c.CategoryId.Equals(id),trackChanges);
        }

        public void UpdateOneCategory(Category entity)=>Update(entity);
    }
}