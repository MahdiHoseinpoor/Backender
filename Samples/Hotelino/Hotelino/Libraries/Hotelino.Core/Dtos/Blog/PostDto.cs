using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Dtos.Blog
{
    public class PostDto : BaseDto
    {

        public string Title { get; set; }
    
        public string Author { get; set; }
    
        public string Content { get; set; }
    
        public CategoryDto CategoryDto { get; set; }
            
    }
}