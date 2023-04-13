//This Class Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
using GoBlog.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GoBlog.Core.Domains.Catalog
{
    public class Comment : BaseEntity
    {
        [Required]
        public  string Content { get; set; }
        [Required]
        public  string Name { get; set; }
        [Required]
        public  CommentStatus CommentStatus { get; set; }
        public  string Email { get; set; }
        public  string PostId { get; set; }
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
    }
}