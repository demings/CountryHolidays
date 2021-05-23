using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CountryHolidays.Models
{
    public class EnricoHoliday
    {
        [JsonPropertyName("date")]
        public EnricoDate Date { get; set; }

        [JsonPropertyName("name")]
        public List<EnricoName> Name { get; set; }
    }

    public class EnricoName
    {
        [JsonPropertyName("lang")]
        public string Lang { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class EnricoDate
    {
        [JsonPropertyName("day")]
        public int Day { get; set; }

        [JsonPropertyName("month")]
        public int Month { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }
    }
}
