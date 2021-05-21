using CountryHolidays.Controllers;
using CountryHolidays.Data;
using CountryHolidays.Models;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using System;
using CountryHolidays.Services;

namespace CountryHolidays.Tests.Controllers
{
    public class MainControllerTests
    {
        private readonly MainController _controller;

        public MainControllerTests()
        {
            var options = new DbContextOptionsBuilder<HolidayContext>().UseInMemoryDatabase("TestDatabase").Options;

            var context = new HolidayContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var countries = new List<Country>
            {
                new Country { Code = "1", Name = "test_1" },
                new Country { Code = "2", Name = "test_2" },
                new Country { Code = "3", Name = "test_3" }
            };

            var holidays = new List<Holiday>
            {
                new Holiday { CountryCode = "1", Name = "holiday_1", Date = DateTime.Parse("2021-02-01") },
                new Holiday { CountryCode = "1", Name = "holiday_2", Date = DateTime.Parse("2021-02-05") },
                new Holiday { CountryCode = "1", Name = "holiday_3", Date = DateTime.Parse("2021-02-06") },
                new Holiday { CountryCode = "1", Name = "holiday_4", Date = DateTime.Parse("2021-05-25") },
                new Holiday { CountryCode = "2", Name = "holiday_5", Date = DateTime.Parse("2021-03-04") },
            };

            context.Countries.AddRange(countries);
            context.Holidays.AddRange(holidays);
            context.SaveChanges();

            var dayService = new DayService(context);

            _controller = new MainController(context, dayService);
        }

        [Fact]
        public async Task GetCountries_ShouldReturnAllCountries()
        {
            var result = await _controller.GetCountries();

            result.Value.Count().ShouldBe(3);

            int i = 1;
            foreach (var value in result.Value)
            {
                value.Name.ShouldBe($"test_{i}");
                i++;
            }
        }

        [Fact]
        public async Task GetHolidays_ShouldReturnGroupedHolidays()
        {
            var result = await _controller.GetHolidays("1", 2021);

            var groups = result.Value.ToList();
            groups.Count.ShouldBe(2);

            groups.ElementAt(0).Key.ShouldBe(2);
            groups.ElementAt(1).Key.ShouldBe(5);

            var firstGroup = groups.ElementAt(0).ToList();
            firstGroup.Count.ShouldBe(3);
            firstGroup[0].Name.ShouldBe("holiday_1");
            firstGroup[1].Name.ShouldBe("holiday_2");
            firstGroup[2].Name.ShouldBe("holiday_3");

            var secondGroup = groups.ElementAt(1).ToList();
            secondGroup.Count.ShouldBe(1);
            secondGroup[0].Name.ShouldBe("holiday_4");
        }

        [Fact]
        public async Task GetDayStatus_ShouldReturnCorrectStatus()
        {
            (await _controller.GetDayStatus("1", 2021, 2, 2)).Value.ShouldBe("Workday");
            (await _controller.GetDayStatus("1", 2021, 2, 6)).Value.ShouldBe("Holiday");
            (await _controller.GetDayStatus("1", 2021, 2, 13)).Value.ShouldBe("Freeday");
        }

        [Fact]
        public async Task GetMaxFreeDays_ShouldReturnLongestFreedayCount()
        {
            var result = await _controller.GetMaxFreeDays("1", 2021);

            result.Value.ShouldBe(3);
        }
    }
}
