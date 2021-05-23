using CountryHolidays.Abstractions;
using CountryHolidays.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using CountryHolidays.Data;

namespace CountryHolidays.Services
{
    public class EnricoService : IEnricoService
    {
        private readonly HttpClient _client = new();
        private readonly HolidayContext _context;

        public EnricoService(HolidayContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetSupportedCountries()
        {
            var streamTask = _client.GetStreamAsync("https://kayaposoft.com/enrico/json/v2.0/?action=getSupportedCountries");
            return await JsonSerializer.DeserializeAsync<IEnumerable<Country>>(await streamTask);
        }

        private async Task<IEnumerable<Holiday>> GetHolidaysForYear(string code, int year)
        {
            var streamTask = _client.GetStreamAsync($"https://kayaposoft.com/enrico/json/v2.0/?action=getHolidaysForYear&year={year}&country={code}&holidayType=public_holiday");
            var holidays = await JsonSerializer.DeserializeAsync<IEnumerable<EnricoHoliday>>(await streamTask);
            return EnricoToHoliday(code, holidays);
        }

        private static IEnumerable<Holiday> EnricoToHoliday(string code, IEnumerable<EnricoHoliday> holidays) =>
            holidays.Select(h => new Holiday 
            { 
                CountryCode = code, 
                Date = new DateTime(h.Date.Year, h.Date.Month, h.Date.Day), 
                Name = h.Name.First().Text 
            });

        public async Task EnsureHolidaysForYearExistInDb(string code, int year)
        {
            var holidays = _context.Holidays.Where(h => h.CountryCode == code && h.Date.Year == year);

            if (!holidays.Any())
            {
                _context.Holidays.AddRange(await GetHolidaysForYear(code, year));
                _context.SaveChanges();
            }
        }
    }
}
