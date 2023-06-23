using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Domains.Blog
{    public class Category : BaseEntity
    {

        [Required]
        [MinLength(0)] [MaxLength(250)]
        public string Title { get; set; } 
    
        public string Description { get; set; } 
    
        public virtual IEnumerable<Post> Posts { get; set; }
            
    }
}