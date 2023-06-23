using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Blog;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Blog
{
    public interface IPostService
    {
        public IList<Post> GetAllPosts();
        public Post GetPostById(string id);
        public bool InsertPost(Post post);
        public bool UpdatePost(Post post);
        public bool DeletePost(Post post);
        public bool DeletePost(string id);
    }
}