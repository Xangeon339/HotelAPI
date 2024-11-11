using HotelAPI.Database;
using HotelAPI.Models;
using HotelAPI.Services.ReportData;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Timers;
namespace HotelAPI.Tasks
{
    public class RabbitMQScheduleTask : IRabbitMQScheduleTask
    {
        private System.Timers.Timer _timer;

        public RabbitMQScheduleTask()
        {
        }

        public void StartTimer()
        {
            _timer = new System.Timers.Timer(5000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
            Console.WriteLine("StartTimer");
        }

        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            var connection = factory.CreateConnection();
                 var channel = connection.CreateModel();

                var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(queue: "task_queue",
                                noAck: true,
                                consumer: consumer);

            consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var request = JsonConvert.DeserializeObject<Report>(message);

                    var builder = new ConfigurationBuilder();
                    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    IConfigurationRoot configuration
                     = builder.Build();

                    var connectionString = configuration.GetConnectionString("DefaultConnection");

                    var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                    optionsBuilder.UseSqlServer(connectionString);

                    using (var context = new DatabaseContext(optionsBuilder.Options))
                    {
                        var dbReport = context.Report.FirstOrDefault(x => x.Uuid == request.Uuid);

                        dbReport.Status = EnmStatusType.Done;

                        context.Update(dbReport);

                        context.SaveChanges();


                    }

                };
            
            /*
            channel.BasicConsume(queue: "task_queue",
                                 noAck: true,
                                 consumer: consumer);
            */

            /*
                        channel.QueueDeclare(queue: "task_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                        Console.WriteLine("Timer başladı.");
                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            Console.WriteLine("veri tabanından önce");
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                            var request = JsonConvert.DeserializeObject<Report>(message);



                            using (context)
                            {
                                var report = context.Report.FirstOrDefault(x => x.Uuid == request.Uuid);

                                report.Status = EnmStatusType.Done;

                                context.Update(report);

                                context.SaveChanges();

                                Console.WriteLine("Status değişti");
                            }
                        };*/
        }

    }
}