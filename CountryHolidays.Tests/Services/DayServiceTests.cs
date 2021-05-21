using System.Linq;
using CountryHolidays.Services;
using Shouldly;
using Xunit;

namespace CountryHolidays.Tests.Services
{
    public class DayServiceTests
    {
        [Fact]
        public void EnumerateDays_ShouldEnumerateAllDay()
        {
            var days = DayService.EnumerateDays(2021).ToList();

            days.Count.ShouldBe(365);

            days.First().Year.ShouldBe(2021);
            days.First().Month.ShouldBe(1);
            days.First().Day.ShouldBe(1);

            days.Last().Year.ShouldBe(2021);
            days.Last().Month.ShouldBe(12);
            days.Last().Day.ShouldBe(31);
        }
    }
}
