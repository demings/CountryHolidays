using CountryHolidays.Models;
using System.Threading.Tasks;

namespace CountryHolidays.Abstractions
{
    public interface IDayStatusService
    {
        Task<DayStatus> GetStatus(string code, int year, int month, int day);
    }
}
