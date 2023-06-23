using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Administrative;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Administrative
{
     public class EmployeesService : IEmployeesService
    {
        #region Fields
        public IRepo<Employees> _employeesRepo;
        #endregion

        #region Ctor
        public EmployeesService(IRepo<Employees> EmployeesRepo)
        {
            _employeesRepo = EmployeesRepo;
        }
        #endregion

        #region Utilities
        #region Employees
        public IList<Employees> GetAllEmployeeses()
        {
            return  _employeesRepo.GetAll().ToList();
        }
        public Employees GetEmployeesById(string id)
        {
            return  _employeesRepo.GetById(id);
        }
        public bool InsertEmployees(Employees employees)
        {
            _employeesRepo.Insert(employees);
            return _employeesRepo.Save();
        }
        public bool UpdateEmployees(Employees employees)
        {
            _employeesRepo.Update(employees);
            return _employeesRepo.Save();
        }
        public bool DeleteEmployees(Employees employees)
        {
            _employeesRepo.Delete(employees);
            return _employeesRepo.Save();
        }
        public bool DeleteEmployees(string id)
        {
            _employeesRepo.Delete(id);
            return _employeesRepo.Save();
        }
        #endregion
        #endregion
    }
}