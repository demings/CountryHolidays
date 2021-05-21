using CountryHolidays.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountryHolidays.Abstractions
{
    public interface IEnricoService
    {
        Task<IEnumerable<Country>> GetSupportedCountries();
    }
}
