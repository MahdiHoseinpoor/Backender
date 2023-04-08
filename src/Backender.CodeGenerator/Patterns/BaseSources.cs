using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeGenerator.Patterns
{
    public static class BaseSources
    {
        public static string ClassSource = @"//This Class Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
$Usings$
namespace $NameSpace$
{
    $AccessModifier$ class $ClassName$
    {
        $InnerObjects$
    }
}";
        public static string InterfaceSource = @"//This Interface Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
$Usings$
namespace $NameSpace$
{
    $AccessModifier$ interface $InterfaceName$
    {
        $InnerObjects$
    }
}";
		public static string EnumSource = @"//This Enum Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
namespace $NameSpace$
{
    $AccessModifier$ enum $EnumName$
    {
        $EnumValues$
    }
}";
		public static string MethodSource = @"$AccessModifier$ $DataType$ $Name$($Parameters$)";

		public static string ConstructorSource = @"$AccessModifier$ $ClassName$($Parameters$)
{
      $InnerCode$
}";
        public static string PropertySource = @"$AccessModifier$ $Modifiers$ $DataType$ $Name$ { get$GetInnerCode$ set$SetInnerCode$ }";
        public static string FieldSource = @"$AccessModifier$ $Modifiers$ $DataType$ $Name$ $DefaultValue$;";

        public static string RepoSource = @"$Usings$
namespace $NameSpace${
    public class Repo<TEntity> : IRepo<TEntity> where TEntity : BaseEntity
    {
        private $Dbcontext$ _context;
        private DbSet<TEntity> _dbset;
        public Repo($Dbcontext$ context)
        {
            _context = context;
            _dbset = _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string includes = """", char includesSplitChar = ',')
        {
            IQueryable<TEntity> query = _dbset;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }
            else
            {
                query = query.OrderByDescending(p => p.CreatedAt_);
            }

            if (includes != """")
            {
                foreach (var include in includes.Split(includesSplitChar))
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }
        public virtual TEntity GetById(string id)
        {
            return _dbset.Find(id);
        }
        public virtual bool Insert(TEntity model)
        {
            try
            {   
                _dbset.Add(model);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public virtual bool Update(TEntity model)
        {
            try
            {
                model.ModifiedAt_ = DateTime.Now;
                if (_context.Entry(model).State == EntityState.Detached)
                {
                    _dbset.Attach(model);
                }
                _context.Entry(model).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
               throw;
            }
        }
        public virtual bool Delete(TEntity model)
        {
            try
            {
                if (_context.Entry(model).State == EntityState.Detached)
                {
                    _dbset.Attach(model);
                }
                _dbset.Remove(model);
                return true;
            }
            catch (Exception)
            {
               throw;
            }
        }
        public virtual bool Delete(string id)
        {
            var entity = GetById(id);
            return Delete(entity);
        }
        public bool Save()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
               throw;
            }
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _dbset;
            var any = query.Any(predicate);
            return any;
        }
    }
}";

        public static string IRepoSource = @"$Usings$
namespace $NameSpace$
{
    public interface IRepo<TEntity>
    {
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where=null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null,
            string includes = """",
            char includesSplitChar = ',');
        public bool Any(Expression<Func<TEntity, bool>> predicate = null);
        public TEntity GetById(string id);
        public bool Insert(TEntity model);
        public bool Update(TEntity model);
        public bool Delete(TEntity model);
        public bool Delete(string id);
 
        public bool Save();
    }
}";
		public static string UnitOfWorkSource = @"//This Class Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
$Usings$
namespace $NameSpace$
{
    $AccessModifier$ class $ClassName$
    {
        $InnerObjects$

		public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}";

	}
}