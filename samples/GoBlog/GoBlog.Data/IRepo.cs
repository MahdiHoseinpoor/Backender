using GoBlog.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GoBlog.Core;
namespace GoBlog.Data
{
    public interface IRepo<TEntity>
    {
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where=null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null,
            string includes = "",
            char includesSplitChar = ',');
        public bool Any(Expression<Func<TEntity, bool>> predicate = null);
        public TEntity GetById(string id);
        public bool Insert(TEntity model);
        public bool Update(TEntity model);
        public bool Delete(TEntity model);
        public bool Delete(string id);
 
        public bool Save();
    }
}