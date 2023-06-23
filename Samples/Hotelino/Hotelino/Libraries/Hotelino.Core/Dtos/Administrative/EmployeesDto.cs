using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hotelino.Core.Dtos.Financial;
namespace Hotelino.Core.Dtos.Administrative
{
    public class EmployeesDto : BaseDto
    {

        public string FirstName { get; set; }
    
        public string LastName { get; set; }
    
        public string Address { get; set; }
    
        public string Email { get; set; }
    
        public string username { get; set; }
    
        public string HashedPasswords { get; set; }
    
        public JobDepartmentDto JobDepartmentDto { get; set; }
            
    }
}