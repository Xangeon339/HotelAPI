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
    public class RabbitMQScheduleTask
    {
        private System.Timers.Timer _timer;

        public RabbitMQScheduleTask()
        {
        }

        public void StartTimer()
        {
            //_timer = new System.Timers.Timer(5000);// 5 sn
            _timer = new System.Timers.Timer(10000);
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
        }

    }
}