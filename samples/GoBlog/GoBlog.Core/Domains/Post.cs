//This Class Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GoBlog.Core.Domains
{
    public class Post:BaseEntity
    {
        [Required]
        public  string Title { get; set; }
        public  string Author { get; set; }
        public  string Content { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
        public  string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}