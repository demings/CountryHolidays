using CountryHolidays.Abstractions;
using CountryHolidays.Data;
using CountryHolidays.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        private readonly IDayService _dayService;

        public MainController(HolidayContext context, IDayService dayService)
        {
            _context = context;
            _dayService = dayService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountries()
        {
            return await _context.Countries.Select(c => CountryToDTO(c)).ToListAsync();
        }

        [HttpGet("{code}/{year}")]
        public async Task<ActionResult<IEnumerable<IGrouping<int, Holiday>>>> GetHolidays(string code, int year)
        {
            var holidays = await _context.Holidays.Where(h => h.CountryCode == code && h.Date.Year == year).ToListAsync();

            return holidays.GroupBy(h => h.Date.Month).ToList();
        }

        [HttpGet("{code}/{year}/{month}/{day}")]
        public async Task<ActionResult<string>> GetDayStatus(string code, int year, int month, int day)
        {
            var date = new DateTime(year, month, day);

            return (await _dayService.GetStatus(code, date)).ToString();
        }

        [HttpGet("{code}/{year}/max")]
        public async Task<ActionResult<int>> GetMaxFreeDays(string code, int year)
        {
            return await _dayService.LongestFreeDaysInARow(code, year);
        }

        private static CountryDTO CountryToDTO(Country country) =>
            new()
            {
                Code = country.Code,
                Name = country.Name
            };
    }
}
