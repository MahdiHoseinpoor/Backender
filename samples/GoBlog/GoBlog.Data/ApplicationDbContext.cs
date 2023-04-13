//This Class Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
using GoBlog.Core;
using Microsoft.EntityFrameworkCore;
using GoBlog.Core.Domains.Catalog;
namespace GoBlog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public  DbSet<Post> Post { get; set; }
        public  DbSet<Comment> Comment { get; set; }
        public  DbSet<Category> Category { get; set; }
    }
}