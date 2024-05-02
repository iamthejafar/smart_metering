using Microsoft.EntityFrameworkCore;

namespace smart_metering.Models
{
    public class DataContext : DbContext
    {


        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<EnergyData> EnergyData { get; set; }
        public DbSet<PowerData> PowerData { get; set; }
        public DbSet<FlowtemperatureData> FlowtemperatureData { get; set; }
        public DbSet<ReturntemperatureData> ReturntemperatureData { get; set; }
        public DbSet<VolumeData> VolumeData { get; set; }
        public DbSet<VolumeflowData> VolumeflowData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=D:\\IOT Project\\smartmeteringdb.db");
        }
    }
}
