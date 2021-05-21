using CountryHolidays.Models;
using System;
using System.Threading.Tasks;

namespace CountryHolidays.Abstractions
{
    public interface IDayService
    {
        Task<DayStatus> GetStatus(string code, DateTime date);
        Task<int> LongestFreeDaysInARow(string code, int year);

    }
}
