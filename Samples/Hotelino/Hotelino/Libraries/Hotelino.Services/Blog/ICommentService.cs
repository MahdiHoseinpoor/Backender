using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Blog;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Blog
{
    public interface ICommentService
    {
        public IList<Comment> GetAllComments();
        public Comment GetCommentById(string id);
        public bool InsertComment(Comment comment);
        public bool UpdateComment(Comment comment);
        public bool DeleteComment(Comment comment);
        public bool DeleteComment(string id);
    }
}