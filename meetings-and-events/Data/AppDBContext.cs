using System.Data;
using meetings_and_events.Models;
using Microsoft.EntityFrameworkCore;

namespace meetings_and_events.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Place> Place { get; set; }
        public DbSet<User_follow> User_follow { get; set; }
        public DbSet<User_join> User_join { get; set; }
        public DbSet<Place_rate> Place_rate { get; set; }
        public DbSet<Place_comments> Place_comments { get; set; }
        public DbSet<Place_address> Place_address { get; set; }
        public DbSet<Place_data_onetime> Place_data_onetime { get; set; }
        public DbSet<Place_data_multitime> Place_data_multitime { get; set; }
        public DbSet<Place_special_close> Place_special_close { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(DatabaseConfig.database());
            base.OnConfiguring(optionsBuilder);
        }
    }
}