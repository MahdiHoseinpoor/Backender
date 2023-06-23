using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Administrative;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Administrative
{
    public interface ICustomerService
    {
        public IList<Customer> GetAllCustomers();
        public Customer GetCustomerById(string id);
        public bool InsertCustomer(Customer customer);
        public bool UpdateCustomer(Customer customer);
        public bool DeleteCustomer(Customer customer);
        public bool DeleteCustomer(string id);
    }
}