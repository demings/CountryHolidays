using CountryHolidays.Data;
using CountryHolidays.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryHolidays.Controllers
{
    [Route("")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly HolidayContext _context;

        public MainController(HolidayContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountries()
        {
            return await _context.Countries.Select(c => CountryToDTO(c)).ToListAsync();
        }

        [HttpGet("{code}/{year}")]
        public async Task<ActionResult<IEnumerable<IGrouping<int, Holiday>>>> GetHolidays(string code, int year)
        {
            var holidays = await _context.Holidays.Where(h => h.Country.Code == code && h.Date.Year == year).ToListAsync();

            return holidays.GroupBy(h => h.Date.Month).ToList();
        }

        [HttpGet("{code}/{year}/{month}/{day}")]
        public async Task<ActionResult<IEnumerable<Country>>> GetDayStatus(string code, int year, int month, int day)
        {
            return await _context.Countries.ToListAsync();
        }

        [HttpGet("{code}/{year}/max")]
        public async Task<ActionResult<IEnumerable<Country>>> GetMaxFreeDays(string code, int year)
        {
            return await _context.Countries.ToListAsync();
        }

        private static CountryDTO CountryToDTO(Country country) =>
            new()
            {
                Code = country.Code,
                Name = country.Name
            };
    }
}
