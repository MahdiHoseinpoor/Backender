//This Class Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
using GoBlog.Core;
using GoBlog.Data;
using GoBlog.Core.Domains;
using Microsoft.EntityFrameworkCore;
namespace GoBlog.Services
{
    public class CategoryService
    {
        public  IRepo<Category> _categoryRepo ;
        public CategoryService(IRepo<Category> CategoryRepo)
        {
            _categoryRepo = CategoryRepo;
        }
        public IList<Category> GetAllCategorys()
        {
            return _categoryRepo.GetAll().ToList();
        }
        public Category GetCategoryById(string id)
        {
            return _categoryRepo.GetById(id);
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
    }
}