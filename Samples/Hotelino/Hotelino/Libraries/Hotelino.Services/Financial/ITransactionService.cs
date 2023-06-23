using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Financial;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Financial
{
    public interface ITransactionService
    {
        public IList<Transaction> GetAllTransactions();
        public Transaction GetTransactionById(string id);
        public bool InsertTransaction(Transaction transaction);
        public bool UpdateTransaction(Transaction transaction);
        public bool DeleteTransaction(Transaction transaction);
        public bool DeleteTransaction(string id);
    }
}