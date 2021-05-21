using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CountryHolidays.Models
{
    public class Country
    {
        [Key]
        [JsonPropertyName("countryCode")]
        public string Code { get; set; }

        [JsonPropertyName("fullName")]
        public string Name { get; set; }

        public ICollection<Holiday> Holidays { get; set; }
    }
}
