using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;
using SzObject.Entity;

namespace SzDataAccess
{
    public class SzContext : DbContext
    {
        public SzContext() { }

        public DbSet<Size> Sizes { get; set; }
        public DbSet<SizeType> SizeTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SZ");
            modelBuilder.Model.GetEntityTypes().ToList().ForEach(item =>
            {
                item.GetProperties().ToList().ForEach(property =>
                {
                    property.SetColumnName(Regex.Replace(property.Name, "(?<!^)([A-Z])", "_$1").ToUpper().Replace("İ", "I"));
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
