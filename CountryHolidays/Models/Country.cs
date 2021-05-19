using System.Collections.Generic;

namespace CountryHolidays.Models
{
    public class Country
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<Holiday> Holidays { get; set; }
    }
}
