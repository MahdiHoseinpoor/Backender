using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Hotelino.Core.Enums;
namespace Hotelino.Core.Dtos.Blog
{
    public class CommentDto : BaseDto
    {

        public string Name { get; set; }
    
        public CommentStatus CommentStatus { get; set; }
    
        public string Email { get; set; }
    
        public string Content { get; set; }
    
        public PostDto PostDto { get; set; }
            
    }
}