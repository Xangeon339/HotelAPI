using HotelAPI.Models;

namespace HotelAPI.Services.ReportData
{
    public interface IReportDataService
    {
        Task<Guid> CreateReportAsync(Hotel hotel);
        IEnumerable<Report> GetAllReports();
        Report GetReport(Guid reportId);

    }
}
