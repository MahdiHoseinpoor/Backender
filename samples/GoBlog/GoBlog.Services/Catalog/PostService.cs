//This Class Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
using GoBlog.Core;
using GoBlog.Data;
using GoBlog.Core.Domains;
using GoBlog.Core.Domains.Catalog;
using Microsoft.EntityFrameworkCore;
namespace GoBlog.Services.Catalog
{
    public class PostService
    {
        public  IRepo<Post> _postRepo ;
        public PostService(IRepo<Post> PostRepo)
        {
            _postRepo = PostRepo;
        }
        public IList<Post> GetAllPosts()
        {
            return _postRepo.GetAll().ToList();
        }
        public Post GetPostById(string id)
        {
            return _postRepo.GetById(id);
        }
        public bool InsertPost(Post post)
        {
            _postRepo.Insert(post);
            return _postRepo.Save();
        }
        public bool UpdatePost(Post post)
        {
            _postRepo.Update(post);
            return _postRepo.Save();
        }
        public bool DeletePost(Post post)
        {
            _postRepo.Delete(post);
            return _postRepo.Save();
        }
        public bool DeletePost(string id)
        {
            _postRepo.Delete(id);
            return _postRepo.Save();
        }
        public List<Post> GetPostsByCategory(string categoryId)
        {
            return _postRepo.GetAll(where: p => p.CategoryId == categoryId).ToList();
        }
        public Post GetPostByAuthor(string author)
        {
            return _postRepo.GetAll(where: p => p.Author == author).FirstOrDefault();
        }
    }
}