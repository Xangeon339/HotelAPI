using HotelAPI.Controllers;
using HotelAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Test
{
    public class ReportControllerTest
    {
        [Test]
        public void CreateReport()
        {

            var controller = new ReportController();

            Guid reportId = Guid.NewGuid();

            var result = controller.CreateReport(reportId);

        }
    }
}
