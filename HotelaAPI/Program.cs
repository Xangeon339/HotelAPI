using HotelAPI.Database;
using HotelAPI.Services.ContactInformation;
using HotelAPI.Services.HotelData;
using HotelAPI.Services.ReportData;
using HotelAPI.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HotelAPI
{
    public class Program
    {        
        public static void Main(string[] args)
        {
            #region appsettings configuration
            var confBuilder = new ConfigurationBuilder();
            confBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration
             = confBuilder.Build();
            #endregion
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var builder = WebApplication.CreateBuilder(args);

            #region Logging





            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IHotelDataService, HotelDataService>();

            builder.Services.AddScoped<IContactInformationService, ContactInformationService>();

            builder.Services.AddScoped<IReportDataService, ReportDataService>();

            var app = builder.Build();

            #region rabbitmq start service
            RabbitMQScheduleTask _timer = new RabbitMQScheduleTask();

            _timer.StartTimer();
            #endregion
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

           
        }
    }
}
