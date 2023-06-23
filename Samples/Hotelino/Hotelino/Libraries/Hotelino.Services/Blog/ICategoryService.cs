using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Blog;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Blog
{
    public interface ICategoryService
    {
        public IList<Category> GetAllCategories();
        public Category GetCategoryById(string id);
        public bool InsertCategory(Category category);
        public bool UpdateCategory(Category category);
        public bool DeleteCategory(Category category);
        public bool DeleteCategory(string id);
    }
}