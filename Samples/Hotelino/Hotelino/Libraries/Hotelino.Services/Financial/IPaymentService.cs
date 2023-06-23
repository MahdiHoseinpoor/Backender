using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Financial;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Financial
{
    public interface IPaymentService
    {
        public IList<Payment> GetAllPayments();
        public Payment GetPaymentById(string id);
        public bool InsertPayment(Payment payment);
        public bool UpdatePayment(Payment payment);
        public bool DeletePayment(Payment payment);
        public bool DeletePayment(string id);
    }
}