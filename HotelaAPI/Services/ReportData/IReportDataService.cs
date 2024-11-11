﻿using HotelAPI.Models;

namespace HotelAPI.Services.ReportData
{
    public interface IReportDataService
    {
        Guid CreateReportAsync(Hotel hotel);
        IEnumerable<Report> GetAllReports();
        Report GetReport(Guid reportId);
        EnmStatusType GetReportStatus(Guid reportId);

    }
}