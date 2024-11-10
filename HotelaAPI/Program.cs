using HotelAPI.Database;
using HotelAPI.Services.ContactInformation;
using HotelAPI.Services.HotelData;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI
{
    public class Program
    {

        const string connectionString = "Data Source=MONSTER\\SQLEXPRESS;Initial Catalog=HotelDB;Integrated Security=True;Pooling=False;Encrypt=False;Trust Server Certificate=False;";
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IHotelDataService, HotelDataService>();

            builder.Services.AddScoped<IContactInformationService, ContactInformationService>();

            var app = builder.Build();

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
