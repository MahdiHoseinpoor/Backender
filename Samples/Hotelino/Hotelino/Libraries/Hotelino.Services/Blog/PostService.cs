using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Blog;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Blog
{
     public class PostService : IPostService
    {
        #region Fields
        public IRepo<Post> _postRepo;
        #endregion

        #region Ctor
        public PostService(IRepo<Post> PostRepo)
        {
            _postRepo = PostRepo;
        }
        #endregion

        #region Utilities
        #region Post
        public IList<Post> GetAllPosts()
        {
            return  _postRepo.GetAll().ToList();
        }
        public Post GetPostById(string id)
        {
            return  _postRepo.GetById(id);
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
        #endregion
        #endregion
    }
}