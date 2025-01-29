using Microsoft.EntityFrameworkCore;

namespace AutoMob_WebAPI.Models
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {

        }

        public DbSet<VehicleModel> Vehicles { get; set;}

        //To connect to the database, configure the context class, use optionsBuilder to specify the connection string.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Employee;Integrated Security=True;Trust Server Certificate=False;");

            }
        }
    }
}
