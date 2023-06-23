using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Administrative;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Administrative
{
    public interface IEmployeesService
    {
        public IList<Employees> GetAllEmployeeses();
        public Employees GetEmployeesById(string id);
        public bool InsertEmployees(Employees employees);
        public bool UpdateEmployees(Employees employees);
        public bool DeleteEmployees(Employees employees);
        public bool DeleteEmployees(string id);
    }
}