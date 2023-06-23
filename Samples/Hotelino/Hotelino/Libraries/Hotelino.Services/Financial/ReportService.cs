using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Financial;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Financial
{
     public class ReportService : IReportService
    {
        #region Fields
        public IRepo<Report> _reportRepo;
        #endregion

        #region Ctor
        public ReportService(IRepo<Report> ReportRepo)
        {
            _reportRepo = ReportRepo;
        }
        #endregion

        #region Utilities
        #region Report
        public IList<Report> GetAllReports()
        {
            return  _reportRepo.GetAll().ToList();
        }
        public Report GetReportById(string id)
        {
            return  _reportRepo.GetById(id);
        }
        public bool InsertReport(Report report)
        {
            _reportRepo.Insert(report);
            return _reportRepo.Save();
        }
        public bool UpdateReport(Report report)
        {
            _reportRepo.Update(report);
            return _reportRepo.Save();
        }
        public bool DeleteReport(Report report)
        {
            _reportRepo.Delete(report);
            return _reportRepo.Save();
        }
        public bool DeleteReport(string id)
        {
            _reportRepo.Delete(id);
            return _reportRepo.Save();
        }
        #endregion
        #endregion
    }
}