using Hotelino.Core.Domains.Financial;
using Hotelino.Core.Dtos.Financial;
using Hotelino.Services.Rooms;
using Hotelino.Services.Administrative;
using Hotelino.Services.Financial;
using Hotelino.Services.Blog;


namespace Hotelino.Services.Financial
{
    public class FinancialDtosFactory
    {
        #region PrepareMethods

            #region Transaction 

        public TransactionDto PrepareTransactionDto(Transaction transaction)
        {
            var transactionDto = new TransactionDto()
            {

                Name = transaction.Name,

                transaction_date = transaction.transaction_date
            };

                        transactionDto.CustomerDto = _administrativeDtosFactory.PrepareCustomerDto(_customerService.GetCustomerById(transaction.CustomerId));
                    
                        transactionDto.EmployeesDto = _administrativeDtosFactory.PrepareEmployeesDto(_employeesService.GetEmployeesById(transaction.EmployeesId));
                    
                        transactionDto.PaymentDto = PreparePaymentDto(_paymentService.GetPaymentById(transaction.PaymentId));
                                
            return transactionDto;
        }
        public List<TransactionDto> PrepareTransactionDto(List<Transaction> Transactions)
        {
            var transactionDtos = new List<TransactionDto>();
            foreach (var transaction in Transactions)
            {
                    transactionDtos.Add(PrepareTransactionDto(transaction));
            }
            return transactionDtos;
        }
            

           #endregion

            #region Payment 

        public PaymentDto PreparePaymentDto(Payment payment)
        {
            var paymentDto = new PaymentDto()
            {

                Payments_date = payment.Payments_date
            };
            
            return paymentDto;
        }
        public List<PaymentDto> PreparePaymentDto(List<Payment> Payments)
        {
            var paymentDtos = new List<PaymentDto>();
            foreach (var payment in Payments)
            {
                    paymentDtos.Add(PreparePaymentDto(payment));
            }
            return paymentDtos;
        }
            

           #endregion

            #region Report 

        public ReportDto PrepareReportDto(Report report)
        {
            var reportDto = new ReportDto()
            {

                information = report.information,

                date = report.date
            };

                        reportDto.TransactionDto = PrepareTransactionDto(_transactionService.GetTransactionById(report.TransactionId));
                                
            return reportDto;
        }
        public List<ReportDto> PrepareReportDto(List<Report> Reports)
        {
            var reportDtos = new List<ReportDto>();
            foreach (var report in Reports)
            {
                    reportDtos.Add(PrepareReportDto(report));
            }
            return reportDtos;
        }
            

           #endregion
        #endregion
        #region fields
        public AdministrativeDtosFactory _administrativeDtosFactory;
        public CustomerService _customerService;
        public EmployeesService _employeesService;
        public PaymentService _paymentService;
        public TransactionService _transactionService;

        #endregion

        #region ctor

        public FinancialDtosFactory(AdministrativeDtosFactory AdministrativeDtosFactory,CustomerService CustomerService,EmployeesService EmployeesService,PaymentService PaymentService,TransactionService TransactionService)
        {
        _administrativeDtosFactory = AdministrativeDtosFactory;
        _customerService = CustomerService;
        _employeesService = EmployeesService;
        _paymentService = PaymentService;
        _transactionService = TransactionService;
        }
          
        #endregion
        
    }
}