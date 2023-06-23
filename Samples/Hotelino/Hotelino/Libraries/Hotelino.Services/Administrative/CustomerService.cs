using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Administrative;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Administrative
{
     public class CustomerService : ICustomerService
    {
        #region Fields
        public IRepo<Customer> _customerRepo;
        #endregion

        #region Ctor
        public CustomerService(IRepo<Customer> CustomerRepo)
        {
            _customerRepo = CustomerRepo;
        }
        #endregion

        #region Utilities
        #region Customer
        public IList<Customer> GetAllCustomers()
        {
            return  _customerRepo.GetAll().ToList();
        }
        public Customer GetCustomerById(string id)
        {
            return  _customerRepo.GetById(id);
        }
        public bool InsertCustomer(Customer customer)
        {
            _customerRepo.Insert(customer);
            return _customerRepo.Save();
        }
        public bool UpdateCustomer(Customer customer)
        {
            _customerRepo.Update(customer);
            return _customerRepo.Save();
        }
        public bool DeleteCustomer(Customer customer)
        {
            _customerRepo.Delete(customer);
            return _customerRepo.Save();
        }
        public bool DeleteCustomer(string id)
        {
            _customerRepo.Delete(id);
            return _customerRepo.Save();
        }
        #endregion
        #endregion
    }
}