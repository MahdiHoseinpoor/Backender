using Hotelino.Core.Domains.Administrative;
using Hotelino.Core.Dtos.Administrative;
using Hotelino.Services.Rooms;
using Hotelino.Services.Administrative;
using Hotelino.Services.Financial;
using Hotelino.Services.Blog;


namespace Hotelino.Services.Administrative
{
    public class AdministrativeDtosFactory
    {
        #region PrepareMethods

            #region Customer 

        public CustomerDto PrepareCustomerDto(Customer customer)
        {
            var customerDto = new CustomerDto()
            {

                FirstName = customer.FirstName,

                LastName = customer.LastName,

                Address = customer.Address,

                Email = customer.Email
            };
            
            return customerDto;
        }
        public List<CustomerDto> PrepareCustomerDto(List<Customer> Customers)
        {
            var customerDtos = new List<CustomerDto>();
            foreach (var customer in Customers)
            {
                    customerDtos.Add(PrepareCustomerDto(customer));
            }
            return customerDtos;
        }
            

           #endregion

            #region Employees 

        public EmployeesDto PrepareEmployeesDto(Employees employees)
        {
            var employeesDto = new EmployeesDto()
            {

                FirstName = employees.FirstName,

                LastName = employees.LastName,

                Address = employees.Address,

                Email = employees.Email,

                username = employees.username,

                HashedPasswords = employees.HashedPasswords
            };

                        employeesDto.JobDepartmentDto = PrepareJobDepartmentDto(_jobDepartmentService.GetJobDepartmentById(employees.JobDepartmentId));
                                
            return employeesDto;
        }
        public List<EmployeesDto> PrepareEmployeesDto(List<Employees> Employeeses)
        {
            var employeesDtos = new List<EmployeesDto>();
            foreach (var employees in Employeeses)
            {
                    employeesDtos.Add(PrepareEmployeesDto(employees));
            }
            return employeesDtos;
        }
            

           #endregion

            #region JobDepartment 

        public JobDepartmentDto PrepareJobDepartmentDto(JobDepartment jobdepartment)
        {
            var jobdepartmentDto = new JobDepartmentDto()
            {

                title = jobdepartment.title
            };
            
            return jobdepartmentDto;
        }
        public List<JobDepartmentDto> PrepareJobDepartmentDto(List<JobDepartment> JobDepartments)
        {
            var jobdepartmentDtos = new List<JobDepartmentDto>();
            foreach (var jobdepartment in JobDepartments)
            {
                    jobdepartmentDtos.Add(PrepareJobDepartmentDto(jobdepartment));
            }
            return jobdepartmentDtos;
        }
            

           #endregion
        #endregion
        #region fields
        public JobDepartmentService _jobDepartmentService;

        #endregion

        #region ctor

        public AdministrativeDtosFactory(JobDepartmentService JobDepartmentService)
        {
        _jobDepartmentService = JobDepartmentService;
        }
          
        #endregion
        
    }
}