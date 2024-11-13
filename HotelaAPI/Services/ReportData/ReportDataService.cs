using HotelAPI.Database;
using HotelAPI.Models;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using System.Threading;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace HotelAPI.Services.ReportData
{
    public class ReportDataService:IReportDataService
    {
        private readonly DatabaseContext context;

        ConnectionFactory factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };


        public ReportDataService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Guid> CreateReportAsync(Guid hotelId)
        {
            using var connection =  factory.CreateConnection();
            using var channel =  connection.CreateModel();

            channel.QueueDeclare(queue: "task_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            using (context)
            {
                Report report = new Report()
                {
                    DateRequested = DateTime.Now,
                    Status = EnmStatusType.InProgress,
                    Uuid = Guid.NewGuid(),
                    HotelId = hotelId
                };

                context.Report.Add(report);

                context.SaveChanges();

                var body = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(report));
                channel.BasicPublish(exchange: "",
                                     routingKey: "task_queue",
                                     basicProperties: null,
                                     body: body);


                return report.Uuid;
            }

        }

        public IEnumerable<Report> GetAllReports()
        {
            using (context)
            {
                var reportList = context.Report.ToList();

                return reportList;
            }
        }

        public Report GetReport(Guid reportId)
        {
            using (context)
            {
                var report = context.Report.FirstOrDefault( x => x.Uuid == reportId);

                if (report == null)
                {
                    throw new Exception("Verilen GUID ye ait bir rapor bulunamadı");
                }

                return report;
            }
        }
    }
}
