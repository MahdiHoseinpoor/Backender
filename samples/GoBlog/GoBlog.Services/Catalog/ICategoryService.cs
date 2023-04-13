//This Interface Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
using GoBlog.Core;
using GoBlog.Data;
using GoBlog.Core.Domains;
using GoBlog.Core.Domains.Catalog;
using Microsoft.EntityFrameworkCore;
namespace GoBlog.Services.Catalog
{
    public interface ICategoryService
    {
        public IList<Category> GetAllCategorys();
        public Category GetCategoryById(string id);
        public bool InsertCategory(Category category);
        public bool UpdateCategory(Category category);
        public bool DeleteCategory(Category category);
        public bool DeleteCategory(string id);
    }
}