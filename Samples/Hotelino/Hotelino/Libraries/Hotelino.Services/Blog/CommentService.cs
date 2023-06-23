using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Blog;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Blog
{
     public class CommentService : ICommentService
    {
        #region Fields
        public IRepo<Comment> _commentRepo;
        #endregion

        #region Ctor
        public CommentService(IRepo<Comment> CommentRepo)
        {
            _commentRepo = CommentRepo;
        }
        #endregion

        #region Utilities
        #region Comment
        public IList<Comment> GetAllComments()
        {
            return  _commentRepo.GetAll().ToList();
        }
        public Comment GetCommentById(string id)
        {
            return  _commentRepo.GetById(id);
        }
        public bool InsertComment(Comment comment)
        {
            _commentRepo.Insert(comment);
            return _commentRepo.Save();
        }
        public bool UpdateComment(Comment comment)
        {
            _commentRepo.Update(comment);
            return _commentRepo.Save();
        }
        public bool DeleteComment(Comment comment)
        {
            _commentRepo.Delete(comment);
            return _commentRepo.Save();
        }
        public bool DeleteComment(string id)
        {
            _commentRepo.Delete(id);
            return _commentRepo.Save();
        }
        #endregion
        #endregion
    }
}