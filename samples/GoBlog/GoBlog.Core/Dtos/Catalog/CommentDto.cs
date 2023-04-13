//This Class Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
using GoBlog.Core.Enums;
namespace GoBlog.Core.Dtos.Catalog
{
    public class CommentDto : BaseDto
    {
        public  string Content { get; set; }
        public  string Name { get; set; }
        public  CommentStatus CommentStatus { get; set; }
        public  string Email { get; set; }
        public  PostDto PostDto { get; set; }
    }
}