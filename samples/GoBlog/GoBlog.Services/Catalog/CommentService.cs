//This Class Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
using GoBlog.Core;
using GoBlog.Data;
using GoBlog.Core.Domains;
using GoBlog.Core.Domains.Catalog;
using Microsoft.EntityFrameworkCore;
namespace GoBlog.Services.Catalog
{
    public class CommentService
    {
        public  IRepo<Comment> _commentRepo ;
        public CommentService(IRepo<Comment> CommentRepo)
        {
            _commentRepo = CommentRepo;
        }
        public IList<Comment> GetAllComments()
        {
            return _commentRepo.GetAll().ToList();
        }
        public Comment GetCommentById(string id)
        {
            return _commentRepo.GetById(id);
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
        public List<Comment> GetCommentsByPost(string postId)
        {
            return _commentRepo.GetAll(where: p => p.PostId == postId).ToList();
        }
    }
}