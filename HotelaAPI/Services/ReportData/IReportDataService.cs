using HotelAPI.Models;

namespace HotelAPI.Services.ReportData
{
    public interface IReportDataService
    {
        Task<Guid> CreateReportAsync(Guid hotelId);
        IEnumerable<Report> GetAllReports();
        Report GetReport(Guid reportId);

    }
}
