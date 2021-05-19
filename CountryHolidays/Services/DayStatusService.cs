using CountryHolidays.Abstractions;
using CountryHolidays.Data;
using CountryHolidays.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CountryHolidays.Services
{

    public class DayStatusService : IDayStatusService
    {
        private readonly HolidayContext _context;

        public DayStatusService(HolidayContext context)
        {
            _context = context;
        }

        public async Task<DayStatus> GetStatus(string code, int year, int month, int day)
        {
            var date = new DateTime(year, month, day);

            var holiday = await _context.Holidays.Where(h => h.CountryCode == code && h.Date == date).FirstOrDefaultAsync();

            if (holiday is null)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    return DayStatus.Freeday;

                return DayStatus.Workday;
            }

            return DayStatus.Holiday;
        }
    }
}
