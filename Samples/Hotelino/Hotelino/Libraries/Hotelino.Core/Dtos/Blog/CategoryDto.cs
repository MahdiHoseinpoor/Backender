using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Dtos.Blog
{
    public class CategoryDto : BaseDto
    {

        public string Title { get; set; }
    
        public string Description { get; set; }
    
    }
}