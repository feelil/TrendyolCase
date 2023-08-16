using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace TrendyolCase.DataAccess
{
    public class TrendyolContext : DbContext
    {
        public DbSet<WebLinkDeepLink> WebLinkDeepLink { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            string cnnString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "Server=postgres_image;Database=trendyoldb;User Id=postgres;Password=postgres;";
            optionsBuilder.UseNpgsql(cnnString);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<WebLinkDeepLink> Students { get; set; }
        public DbSet<RegexPattern> RegexPattern { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

   
    
}
