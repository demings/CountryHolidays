using CountryHolidays.Models;
using Microsoft.EntityFrameworkCore;

namespace CountryHolidays.Data
{
    public class HolidayContext : DbContext
    {
        public HolidayContext(DbContextOptions<HolidayContext> options) : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<Holiday>().ToTable("Holiday");
        }
    }
}
