using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Blog;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Blog
{
     public class CategoryService : ICategoryService
    {
        #region Fields
        public IRepo<Category> _categoryRepo;
        #endregion

        #region Ctor
        public CategoryService(IRepo<Category> CategoryRepo)
        {
            _categoryRepo = CategoryRepo;
        }
        #endregion

        #region Utilities
        #region Category
        public IList<Category> GetAllCategories()
        {
            return  _categoryRepo.GetAll().ToList();
        }
        public Category GetCategoryById(string id)
        {
            return  _categoryRepo.GetById(id);
        }
        public bool InsertCategory(Category category)
        {
            _categoryRepo.Insert(category);
            return _categoryRepo.Save();
        }
        public bool UpdateCategory(Category category)
        {
            _categoryRepo.Update(category);
            return _categoryRepo.Save();
        }
        public bool DeleteCategory(Category category)
        {
            _categoryRepo.Delete(category);
            return _categoryRepo.Save();
        }
        public bool DeleteCategory(string id)
        {
            _categoryRepo.Delete(id);
            return _categoryRepo.Save();
        }
        #endregion
        #endregion
    }
}