using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Administrative;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Administrative
{
    public interface IJobDepartmentService
    {
        public IList<JobDepartment> GetAllJobDepartments();
        public JobDepartment GetJobDepartmentById(string id);
        public bool InsertJobDepartment(JobDepartment jobdepartment);
        public bool UpdateJobDepartment(JobDepartment jobdepartment);
        public bool DeleteJobDepartment(JobDepartment jobdepartment);
        public bool DeleteJobDepartment(string id);
    }
}