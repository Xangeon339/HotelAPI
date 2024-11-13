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

                    var connectionString = configuration.GetConnectionString("MSSQL");

                    var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

                    optionsBuilder.UseSqlServer(connectionString);

                    using (var context = new DatabaseContext(optionsBuilder.Options))
                    {
                        var dbReport = context.Report.FirstOrDefault(x => x.Uuid == request.Uuid);

                        var allContactLocationList = context.ContactInformation.Where(x => x.InformationType == EnmInformationType.Location) ;

                        var dbContactInformationLocationList = allContactLocationList.Where(x => (x.HotelUuid == dbReport.HotelId) && (x.InformationType == EnmInformationType.Location));

                        int regHotCount = 0, regPhoCount = 0;

                        if(dbContactInformationLocationList != null)
                        {
                            foreach(ContactInformation conInfo in dbContactInformationLocationList)
                            {
                                Location loc = JsonConvert.DeserializeObject<Location>(conInfo.InformationContent);

                                foreach (ContactInformation conInfoAll in allContactLocationList)
                                {
                                    Location locAll = JsonConvert.DeserializeObject<Location>(conInfoAll.InformationContent);
                                    if (CalculateDistance(loc.Latitude,loc.Longitude,locAll.Latitude,locAll.Longitude) < 500)
                                    {
                                        regHotCount++;

                                        regPhoCount += context.ContactInformation.Where(x => (x.HotelUuid == conInfoAll.HotelUuid) && (x.InformationType == EnmInformationType.PhoneNumber)).Count();

                                    }
                                }

                                
                            }
                        }

                        dbReport.Status = EnmStatusType.Done;

                        Console.WriteLine(JsonConvert.SerializeObject(dbReport));

                        context.Update(dbReport);

                        context.SaveChanges();

                    }

                };
        }


        public double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            // İki nokta arasındaki uzaklık formülü:
            // √((x2 - x1)^2 + (y2 - y1)^2)

            double deltaX = x2 - x1;
            double deltaY = y2 - y1;
            double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            return distance;
        }

    }
}