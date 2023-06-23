using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Domains.Blog
{    public class Post : BaseEntity
    {

        [Required]
        [MinLength(0)] [MaxLength(250)]
        public string Title { get; set; } 
    
        [Required]
        [MinLength(0)] [MaxLength(250)]
        public string Author { get; set; } 
    
        public string Content { get; set; } 
    
        public virtual IEnumerable<Comment> Comments { get; set; }
            
        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
            
    }
}