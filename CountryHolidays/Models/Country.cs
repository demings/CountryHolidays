using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CountryHolidays.Models
{
    public class Country
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<Holiday> Holidays { get; set; }
    }
}
