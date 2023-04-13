//This Class Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
using GoBlog.Core.Enums;
namespace GoBlog.Core.Dtos.Catalog
{
    public class PostDto : BaseDto
    {
        public  string Title { get; set; }
        public  string Author { get; set; }
        public  string Content { get; set; }
        public  CategoryDto CategoryDto { get; set; }
    }
}