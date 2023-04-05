//This Class Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
using System.ComponentModel.DataAnnotations;
namespace GoBlog.Core
{
    public class BaseEntity
    {
        [Key]
        public  string Id { get; set; }
        public  DateTime CreatedAt_ { get; set; }
        public  DateTime ModifiedAt_ { get; set; }
    }
}