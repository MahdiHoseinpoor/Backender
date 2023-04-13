//This Class Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
using GoBlog.Services.Catalog;
using GoBlog.Core.Domains.Catalog;
using Microsoft.EntityFrameworkCore;
using GoBlog.Core;
using GoBlog.Data;
namespace GoBlog.Services
{
    public class UnitOfWork : IDisposable
    {
        private  PostService _postservice ;
        private  CommentService _commentservice ;
        private  CategoryService _categoryservice ;
        private  ApplicationDbContext _context ;
        private  Repo<Post> _postRepo ;
        private  Repo<Comment> _commentRepo ;
        private  Repo<Category> _categoryRepo ;
        private  CatalogDtosFactory _catalogdtosfactory ;
        public UnitOfWork(ApplicationDbContext Context)
        {
            _context = Context;
        }
        public  PostService PostService
        {
            get
            {
                if (_postservice == null)
                {
                    _postservice = new PostService(PostRepo);
                }
                return _postservice;
            }
        }
        public  CommentService CommentService
        {
            get
            {
                if (_commentservice == null)
                {
                    _commentservice = new CommentService(CommentRepo);
                }
                return _commentservice;
            }
        }
        public  CategoryService CategoryService
        {
            get
            {
                if (_categoryservice == null)
                {
                    _categoryservice = new CategoryService(CategoryRepo);
                }
                return _categoryservice;
            }
        }
        public  Repo<Post> PostRepo
        {
            get
            {
                if (_postRepo == null)
                {
                    _postRepo = new Repo<Post>(_context);
                }
                return _postRepo;
            }
        }
        public  Repo<Comment> CommentRepo
        {
            get
            {
                if (_commentRepo == null)
                {
                    _commentRepo = new Repo<Comment>(_context);
                }
                return _commentRepo;
            }
        }
        public  Repo<Category> CategoryRepo
        {
            get
            {
                if (_categoryRepo == null)
                {
                    _categoryRepo = new Repo<Category>(_context);
                }
                return _categoryRepo;
            }
        }
        public  CatalogDtosFactory CatalogDtosFactory
        {
            get
            {
                if (_catalogdtosfactory == null)
                {
                    _catalogdtosfactory = new CatalogDtosFactory(CategoryService,PostService);
                }
                return _catalogdtosfactory;
            }
        }
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
}