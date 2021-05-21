using CountryHolidays.Abstractions;
using CountryHolidays.Data;
using CountryHolidays.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryHolidays.Services
{

    public class DayService : IDayService
    {
        private readonly HolidayContext _context;

        public DayService(HolidayContext context)
        {
            _context = context;
        }

        public async Task<DayStatus> GetStatus(string code, DateTime date)
        {
            var holiday = await _context.Holidays.Where(h => h.CountryCode == code && h.Date == date).FirstOrDefaultAsync();

            if (holiday is null)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    return DayStatus.Freeday;

                return DayStatus.Workday;
            }

            return DayStatus.Holiday;
        }

        public static IEnumerable<DateTime> EnumerateDays(int year)
        {
            return Enumerable
                .Range(1, DateTime.IsLeapYear(year) ? 366 : 365)
                .Select(i => new DateTime(year, 1, 1).AddDays(i - 1));
        }

        public async Task<int> LongestFreeDaysInARow(string code, int year)
        {
            int longestCount = 0;
            int freeCount = 0;

            foreach (var day in EnumerateDays(year))
            {
                var status = await GetStatus(code, day);

                switch (status)
                {
                    case DayStatus.Workday:
                        freeCount = 0;
                        break;
                    case DayStatus.Freeday:
                    case DayStatus.Holiday:
                        freeCount++;
                        break;
                    default:
                        break;
                }

                if (freeCount > longestCount)
                    longestCount = freeCount;
            }

            return longestCount;
        }
    }
}
