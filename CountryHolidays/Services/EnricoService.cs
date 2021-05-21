using CountryHolidays.Abstractions;
using CountryHolidays.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CountryHolidays.Services
{
    public class EnricoService : IEnricoService
    {
        private readonly HttpClient _client = new();

        public async Task<IEnumerable<Country>> GetSupportedCountries()
        {
            var streamTask = _client.GetStreamAsync("https://kayaposoft.com/enrico/json/v2.0/?action=getSupportedCountries");
            return await JsonSerializer.DeserializeAsync<IEnumerable<Country>>(await streamTask);
        }
    }
}
