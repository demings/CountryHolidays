using CountryHolidays.Models;
using System;
using System.Linq;

namespace CountryHolidays.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HolidayContext context)
        {
            context.Database.EnsureCreated();

            // Look for any countries.
            if (context.Countries.Any())
            {
                return;   // DB has been seeded
            }

            var countries = new Country[]
            {
                new Country { Code = "ago", Name = "Angola" },
                new Country { Code = "aus", Name = "Australia" }
            };
            foreach (var c in countries)
            {
                context.Countries.Add(c);
            }
            context.SaveChanges();

            var holidays = new Holiday[]
            {
                new Holiday { CountryCode = "ago", Date = DateTime.Parse("2022-01-01"), Name = "Ano Novo" },
                new Holiday { CountryCode = "ago", Date = DateTime.Parse("2022-01-25"), Name = "Dia da Cidade de Luanda" },
                new Holiday { CountryCode = "ago", Date = DateTime.Parse("2022-02-04"), Name = "Dia Nacional do Esforço Armado" },
                new Holiday { CountryCode = "aus", Date = DateTime.Parse("2022-01-01"), Name = "New Year's Day" }
            };
            foreach (var h in holidays)
            {
                context.Holidays.Add(h);
            }
            context.SaveChanges();
        }
    }
}
