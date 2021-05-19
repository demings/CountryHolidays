using System;

namespace CountryHolidays.Models
{
    public class Holiday
    {
        public int ID { get; set; }
        public string CountryCode { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Country Country { get; set; }
    }
}
