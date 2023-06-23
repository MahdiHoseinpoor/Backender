using Hotelino.Core;
using Microsoft.EntityFrameworkCore;
using Hotelino.Core.Domains.Administrative;
using Hotelino.Core.Domains.Rooms;
using Hotelino.Core.Domains.Financial;
using Hotelino.Core.Domains.Blog;
using Hotelino.Core.Domains;
namespace Hotelino.Data
{  
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<RoomClass> RoomClass { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<JobDepartment> JobDepartment { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Category> Category { get; set; }
    }
}