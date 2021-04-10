using BecomexChallenge.Domain.Models;
using System.Collections.Generic;

namespace BecomexChallenge.Domain.DTO
{
    public class CountryDTO
    {
        public string? Name { get; set; }
        public string? Cioc { get; set; }
        public string? Alpha3Code { get; set; }
        public string? Flag { get; set; }
        public string? Capital { get; set; }
        public int? Population { get; set; }
        public List<string> Borders { get; set; }
        public List<string> Timezones { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<Language> Languages { get; set; }
        public List<RegionalBloc> RegionalBlocs { get; set; }
    }
}
