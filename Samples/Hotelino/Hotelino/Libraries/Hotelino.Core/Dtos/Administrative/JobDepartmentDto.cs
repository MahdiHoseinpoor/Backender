using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Dtos.Administrative
{
    public class JobDepartmentDto : BaseDto
    {

        public string title { get; set; }
    
    }
}