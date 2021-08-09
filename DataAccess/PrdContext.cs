using Microsoft.EntityFrameworkCore;
using Object.Entity;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataAccess
{
    public class PrdContext : DbContext
    {
        public PrdContext() { }
        public PrdContext(DbContextOptions<PrdContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("PRD");
            modelBuilder.Model.GetEntityTypes().ToList().ForEach(item =>
            {
                item.GetProperties().ToList().ForEach(property =>
                {
                    property.SetColumnName(Regex.Replace(property.Name, "(?<!^)([A-Z])", "_$1").ToUpper().Replace("İ","I"));
                    if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
                    {
                        property.SetPrecision(18);
                        property.SetPrecision(6);
                    }
                });
            });

        }
    }
}
