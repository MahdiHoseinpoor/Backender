using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Financial;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Financial
{
     public class PaymentService : IPaymentService
    {
        #region Fields
        public IRepo<Payment> _paymentRepo;
        #endregion

        #region Ctor
        public PaymentService(IRepo<Payment> PaymentRepo)
        {
            _paymentRepo = PaymentRepo;
        }
        #endregion

        #region Utilities
        #region Payment
        public IList<Payment> GetAllPayments()
        {
            return  _paymentRepo.GetAll().ToList();
        }
        public Payment GetPaymentById(string id)
        {
            return  _paymentRepo.GetById(id);
        }
        public bool InsertPayment(Payment payment)
        {
            _paymentRepo.Insert(payment);
            return _paymentRepo.Save();
        }
        public bool UpdatePayment(Payment payment)
        {
            _paymentRepo.Update(payment);
            return _paymentRepo.Save();
        }
        public bool DeletePayment(Payment payment)
        {
            _paymentRepo.Delete(payment);
            return _paymentRepo.Save();
        }
        public bool DeletePayment(string id)
        {
            _paymentRepo.Delete(id);
            return _paymentRepo.Save();
        }
        #endregion
        #endregion
    }
}