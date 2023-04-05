//This Interface Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
using GoBlog.Core;
using GoBlog.Data;
using GoBlog.Core.Domains;
using Microsoft.EntityFrameworkCore;
namespace GoBlog.Services
{
    public interface IPostService
    {
        public IList<Post> GetAllPosts();
        public Post GetPostById(string id);
        public bool InsertPost(Post post);
        public bool UpdatePost(Post post);
        public bool DeletePost(Post post);
        public bool DeletePost(string id);
        public Post GetPostByAuthor(string author);
        public List<Post> GetPostsByCategory(string categoryId);
        public Post GetPostByAuthor(string author);
    }
}