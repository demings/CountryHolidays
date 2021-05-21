using CountryHolidays.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace CountryHolidays.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(HolidayContext context, IEnricoService enricoService)
        {
            context.Database.EnsureCreated();

            // Look for any countries.
            if (context.Countries.Any())
            {
                return;   // DB has been seeded
            }

            context.Countries.AddRange(await enricoService.GetSupportedCountries());
            context.SaveChanges();
        }
    }
}
