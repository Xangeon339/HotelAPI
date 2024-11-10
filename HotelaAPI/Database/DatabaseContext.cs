using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace HotelAPI.Database
{
    public class DatabaseContext : DbContext
    {
        const string connectionString = "Data Source=MONSTER\\SQLEXPRESS;Initial Catalog=HotelDB;Integrated Security=True;Pooling=False;Encrypt=False;Trust Server Certificate=False;";

        public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions) : base(dbContextOptions) 
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<ContactInformation> ContactInformation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
