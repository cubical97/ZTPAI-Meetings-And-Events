using System.Data;
using meetings_and_events.Models;
using Microsoft.EntityFrameworkCore;

namespace meetings_and_events.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Users> users { get; set; }
        public DbSet<Place> place { get; set; }
        public DbSet<User_follow> user_follow { get; set; }
        public DbSet<User_join> user_join { get; set; }
        public DbSet<Place_rate> place_rate { get; set; }
        public DbSet<Place_comments> place_comments { get; set; }
        public DbSet<Place_address> place_address { get; set; }
        public DbSet<Place_data_onetime> place_data_onetime { get; set; }
        public DbSet<Place_data_multitime> place_data_multitime { get; set; }
        public DbSet<Place_special_close> place_special_close { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(DatabaseConfig.database());
            base.OnConfiguring(optionsBuilder);
        }
    }
}