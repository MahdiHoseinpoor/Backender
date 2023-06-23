using Microsoft.EntityFrameworkCore;
using Hotelino.Data;
using Hotelino.Core;


using Hotelino.Core.Domains.Administrative;
using Hotelino.Services.Administrative;
using Hotelino.Core.Domains.Rooms;
using Hotelino.Services.Rooms;
using Hotelino.Core.Domains.Financial;
using Hotelino.Services.Financial;
using Hotelino.Core.Domains.Blog;
using Hotelino.Services.Blog;
namespace Hotelino.Services
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext _context;
        
        public UnitOfWork(ApplicationDbContext Context)
        {
            _context = Context;
        }


        private CustomerService _customerService;
        public CustomerService CustomerService
        {
            get
            {
                if (_customerService == null)
                {
                    _customerService = new CustomerService(_customerRepo);
                }
                return _customerService;
            }
        }
                    
        private Repo<Customer> _customerRepo;
        public  Repo<Customer> CustomerRepo
        {
            get
            {
                if (_customerRepo == null)
                {
                    _customerRepo = new Repo<Customer>(_context);
                }
                return _customerRepo;
            }
        }
        

        private RoomClassService _roomClassService;
        public RoomClassService RoomClassService
        {
            get
            {
                if (_roomClassService == null)
                {
                    _roomClassService = new RoomClassService(_roomClassRepo);
                }
                return _roomClassService;
            }
        }
                    
        private Repo<RoomClass> _roomClassRepo;
        public  Repo<RoomClass> RoomClassRepo
        {
            get
            {
                if (_roomClassRepo == null)
                {
                    _roomClassRepo = new Repo<RoomClass>(_context);
                }
                return _roomClassRepo;
            }
        }
        

        private RoomService _roomService;
        public RoomService RoomService
        {
            get
            {
                if (_roomService == null)
                {
                    _roomService = new RoomService(_roomRepo);
                }
                return _roomService;
            }
        }
                    
        private Repo<Room> _roomRepo;
        public  Repo<Room> RoomRepo
        {
            get
            {
                if (_roomRepo == null)
                {
                    _roomRepo = new Repo<Room>(_context);
                }
                return _roomRepo;
            }
        }
        

        private TransactionService _transactionService;
        public TransactionService TransactionService
        {
            get
            {
                if (_transactionService == null)
                {
                    _transactionService = new TransactionService(_transactionRepo);
                }
                return _transactionService;
            }
        }
                    
        private Repo<Transaction> _transactionRepo;
        public  Repo<Transaction> TransactionRepo
        {
            get
            {
                if (_transactionRepo == null)
                {
                    _transactionRepo = new Repo<Transaction>(_context);
                }
                return _transactionRepo;
            }
        }
        

        private PaymentService _paymentService;
        public PaymentService PaymentService
        {
            get
            {
                if (_paymentService == null)
                {
                    _paymentService = new PaymentService(_paymentRepo);
                }
                return _paymentService;
            }
        }
                    
        private Repo<Payment> _paymentRepo;
        public  Repo<Payment> PaymentRepo
        {
            get
            {
                if (_paymentRepo == null)
                {
                    _paymentRepo = new Repo<Payment>(_context);
                }
                return _paymentRepo;
            }
        }
        

        private ReportService _reportService;
        public ReportService ReportService
        {
            get
            {
                if (_reportService == null)
                {
                    _reportService = new ReportService(_reportRepo);
                }
                return _reportService;
            }
        }
                    
        private Repo<Report> _reportRepo;
        public  Repo<Report> ReportRepo
        {
            get
            {
                if (_reportRepo == null)
                {
                    _reportRepo = new Repo<Report>(_context);
                }
                return _reportRepo;
            }
        }
        

        private EmployeesService _employeesService;
        public EmployeesService EmployeesService
        {
            get
            {
                if (_employeesService == null)
                {
                    _employeesService = new EmployeesService(_employeesRepo);
                }
                return _employeesService;
            }
        }
                    
        private Repo<Employees> _employeesRepo;
        public  Repo<Employees> EmployeesRepo
        {
            get
            {
                if (_employeesRepo == null)
                {
                    _employeesRepo = new Repo<Employees>(_context);
                }
                return _employeesRepo;
            }
        }
        

        private JobDepartmentService _jobDepartmentService;
        public JobDepartmentService JobDepartmentService
        {
            get
            {
                if (_jobDepartmentService == null)
                {
                    _jobDepartmentService = new JobDepartmentService(_jobDepartmentRepo);
                }
                return _jobDepartmentService;
            }
        }
                    
        private Repo<JobDepartment> _jobDepartmentRepo;
        public  Repo<JobDepartment> JobDepartmentRepo
        {
            get
            {
                if (_jobDepartmentRepo == null)
                {
                    _jobDepartmentRepo = new Repo<JobDepartment>(_context);
                }
                return _jobDepartmentRepo;
            }
        }
        

        private PostService _postService;
        public PostService PostService
        {
            get
            {
                if (_postService == null)
                {
                    _postService = new PostService(_postRepo);
                }
                return _postService;
            }
        }
                    
        private Repo<Post> _postRepo;
        public  Repo<Post> PostRepo
        {
            get
            {
                if (_postRepo == null)
                {
                    _postRepo = new Repo<Post>(_context);
                }
                return _postRepo;
            }
        }
        

        private CommentService _commentService;
        public CommentService CommentService
        {
            get
            {
                if (_commentService == null)
                {
                    _commentService = new CommentService(_commentRepo);
                }
                return _commentService;
            }
        }
                    
        private Repo<Comment> _commentRepo;
        public  Repo<Comment> CommentRepo
        {
            get
            {
                if (_commentRepo == null)
                {
                    _commentRepo = new Repo<Comment>(_context);
                }
                return _commentRepo;
            }
        }
        

        private CategoryService _categoryService;
        public CategoryService CategoryService
        {
            get
            {
                if (_categoryService == null)
                {
                    _categoryService = new CategoryService(_categoryRepo);
                }
                return _categoryService;
            }
        }
                    
        private Repo<Category> _categoryRepo;
        public  Repo<Category> CategoryRepo
        {
            get
            {
                if (_categoryRepo == null)
                {
                    _categoryRepo = new Repo<Category>(_context);
                }
                return _categoryRepo;
            }
        }
        
        private AdministrativeDtosFactory _administrativeDtosFactory;
        public AdministrativeDtosFactory AdministrativeDtosFactory
        {
            get
            {
                if (_administrativeDtosFactory == null)
                {
                     _administrativeDtosFactory = new AdministrativeDtosFactory(JobDepartmentService);
                }
                return _administrativeDtosFactory;
            }
        }
        private RoomsDtosFactory _roomsDtosFactory;
        public RoomsDtosFactory RoomsDtosFactory
        {
            get
            {
                if (_roomsDtosFactory == null)
                {
                     _roomsDtosFactory = new RoomsDtosFactory(RoomClassService);
                }
                return _roomsDtosFactory;
            }
        }
        private FinancialDtosFactory _financialDtosFactory;
        public FinancialDtosFactory FinancialDtosFactory
        {
            get
            {
                if (_financialDtosFactory == null)
                {
                     _financialDtosFactory = new FinancialDtosFactory(AdministrativeDtosFactory,CustomerService,EmployeesService,PaymentService,TransactionService);
                }
                return _financialDtosFactory;
            }
        }
        private BlogDtosFactory _blogDtosFactory;
        public BlogDtosFactory BlogDtosFactory
        {
            get
            {
                if (_blogDtosFactory == null)
                {
                     _blogDtosFactory = new BlogDtosFactory(CategoryService,PostService);
                }
                return _blogDtosFactory;
            }
        }        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}