using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Hotelino.Core.Enums;
namespace Hotelino.Core.Domains.Blog
{    public class Comment : BaseEntity
    {

        [Required]
        [MinLength(0)] [MaxLength(250)]
        public string Name { get; set; } 
    
        public CommentStatus CommentStatus { get; set; } 
    
        [Required]
        [EmailAddress]
        public string Email { get; set; } 
    
        public string Content { get; set; } 
    
        public string PostId { get; set; }
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
            
    }
}