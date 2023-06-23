using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Administrative;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Administrative
{
     public class JobDepartmentService : IJobDepartmentService
    {
        #region Fields
        public IRepo<JobDepartment> _jobDepartmentRepo;
        #endregion

        #region Ctor
        public JobDepartmentService(IRepo<JobDepartment> JobDepartmentRepo)
        {
            _jobDepartmentRepo = JobDepartmentRepo;
        }
        #endregion

        #region Utilities
        #region JobDepartment
        public IList<JobDepartment> GetAllJobDepartments()
        {
            return  _jobDepartmentRepo.GetAll().ToList();
        }
        public JobDepartment GetJobDepartmentById(string id)
        {
            return  _jobDepartmentRepo.GetById(id);
        }
        public bool InsertJobDepartment(JobDepartment jobdepartment)
        {
            _jobDepartmentRepo.Insert(jobdepartment);
            return _jobDepartmentRepo.Save();
        }
        public bool UpdateJobDepartment(JobDepartment jobdepartment)
        {
            _jobDepartmentRepo.Update(jobdepartment);
            return _jobDepartmentRepo.Save();
        }
        public bool DeleteJobDepartment(JobDepartment jobdepartment)
        {
            _jobDepartmentRepo.Delete(jobdepartment);
            return _jobDepartmentRepo.Save();
        }
        public bool DeleteJobDepartment(string id)
        {
            _jobDepartmentRepo.Delete(id);
            return _jobDepartmentRepo.Save();
        }
        #endregion
        #endregion
    }
}