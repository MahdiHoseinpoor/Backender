using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Financial;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Financial
{
    public interface IReportService
    {
        public IList<Report> GetAllReports();
        public Report GetReportById(string id);
        public bool InsertReport(Report report);
        public bool UpdateReport(Report report);
        public bool DeleteReport(Report report);
        public bool DeleteReport(string id);
    }
}