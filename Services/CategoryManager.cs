using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public CategoryManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public void CreateCategory(CategoryDtoForInsertion categoryDto)
        {
            Category category=_mapper.Map<Category>(categoryDto);   
            _manager.Category.Create(category);
            _manager.Save();
        }

        public void DeleteOneCategory(int id)
        {
            Category category = GetOneCategory(id, false);
            if (category is not null)
            {
                _manager.Category.DeleteOneCategory(category);
                _manager.Save();
            }
        }

        public IEnumerable<Category> GetAllCategories(bool trackChanges)
        {
            return _manager.Category.FindAll(trackChanges);
        }

        public Category? GetOneCategory(int id, bool trackChanges)
        {
            var category=_manager.Category.GetOneCategory(id, trackChanges);
            if (category is null)
            {
                throw new Exception("Product not found!");
            }
            return category;
        }

        public CategoryDtoForUpdate GetOneCategoryForUpdate(int id, bool trackChanges)
        {
            var category=GetOneCategory(id, trackChanges);
            var categoryDto=_mapper.Map<CategoryDtoForUpdate>(category);
            return categoryDto;
        }

        public void UpdateOneCategory(CategoryDtoForUpdate categoryDto)
        {
            var entity=_mapper.Map<Category>(categoryDto);
            _manager.Category.UpdateOneCategory(entity);
            _manager.Save();
        }
    }
}