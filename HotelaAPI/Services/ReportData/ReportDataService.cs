using HotelAPI.Database;
using HotelAPI.Models;

namespace HotelAPI.Services.ReportData
{
    public class ReportDataService:IReportDataService
    {
        private readonly DatabaseContext context;

        public ReportDataService(DatabaseContext context)
        {
            this.context = context;
        }

        public Guid CreateReportAsync(Hotel hotel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Report> GetAllReports()
        {
            throw new NotImplementedException();
        }

        public Report GetReport(Guid reportId)
        {
            throw new NotImplementedException();
        }

        public EnmStatusType GetReportStatus(Guid reportId)
        {
            throw new NotImplementedException();
        }
    }
}
