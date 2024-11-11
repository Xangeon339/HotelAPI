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

        public async Task<Guid> CreateReportAsync(Hotel hotel)
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
                    Latitude = hotel.Latitude,
                    Longitude = hotel.Longitude,
                    Status = EnmStatusType.InProgress,
                    Uuid = Guid.NewGuid()
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
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var
 connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "task_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var numbers = message.Split(',');
                int num1 = int.Parse(numbers[0]);
                int num2 = int.Parse(numbers[1]);
                int result = num1 + num2;

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queue: "task_queue", consumer: consumer);


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

        public void CheckQueque(Report report)
        {
           
            using (context)
            {
                var dbReport = context.Report.FirstOrDefault(x => x.Uuid == report.Uuid);

                dbReport.Status = EnmStatusType.Done;

                context.Update(dbReport);

                context.SaveChanges();


            }
        }
    }
}
