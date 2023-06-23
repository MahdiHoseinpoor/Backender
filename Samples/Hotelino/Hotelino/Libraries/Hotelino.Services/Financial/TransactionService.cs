using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Financial;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Financial
{
     public class TransactionService : ITransactionService
    {
        #region Fields
        public IRepo<Transaction> _transactionRepo;
        #endregion

        #region Ctor
        public TransactionService(IRepo<Transaction> TransactionRepo)
        {
            _transactionRepo = TransactionRepo;
        }
        #endregion

        #region Utilities
        #region Transaction
        public IList<Transaction> GetAllTransactions()
        {
            return  _transactionRepo.GetAll().ToList();
        }
        public Transaction GetTransactionById(string id)
        {
            return  _transactionRepo.GetById(id);
        }
        public bool InsertTransaction(Transaction transaction)
        {
            _transactionRepo.Insert(transaction);
            return _transactionRepo.Save();
        }
        public bool UpdateTransaction(Transaction transaction)
        {
            _transactionRepo.Update(transaction);
            return _transactionRepo.Save();
        }
        public bool DeleteTransaction(Transaction transaction)
        {
            _transactionRepo.Delete(transaction);
            return _transactionRepo.Save();
        }
        public bool DeleteTransaction(string id)
        {
            _transactionRepo.Delete(id);
            return _transactionRepo.Save();
        }
        #endregion
        #endregion
    }
}