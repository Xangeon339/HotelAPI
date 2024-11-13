using HotelAPI.Models;
using HotelAPI.Services.HotelData;
using HotelAPI.Services.ReportData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportDataService reportDataService;
        public ReportController(IReportDataService reportDataService)
        {
            this.reportDataService = reportDataService;
        }

        [HttpPost]
        public IActionResult CreateReport([FromBody] Guid hotelId)
        {

            try
            {
                var result = reportDataService.CreateReportAsync(hotelId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
