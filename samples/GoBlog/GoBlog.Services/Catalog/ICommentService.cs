//This Interface Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
using GoBlog.Core;
using GoBlog.Data;
using GoBlog.Core.Domains;
using GoBlog.Core.Domains.Catalog;
using Microsoft.EntityFrameworkCore;
namespace GoBlog.Services.Catalog
{
    public interface ICommentService
    {
        public IList<Comment> GetAllComments();
        public Comment GetCommentById(string id);
        public bool InsertComment(Comment comment);
        public bool UpdateComment(Comment comment);
        public bool DeleteComment(Comment comment);
        public bool DeleteComment(string id);
        public List<Comment> GetCommentsByPost(string postId);
    }
}