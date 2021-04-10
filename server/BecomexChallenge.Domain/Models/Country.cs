using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecomexChallenge.Domain.Models
{
    public class Country
    {
        public string? Name { get; set; }
        public string? Alpha2Code { get; set; }
        public string? Alpha3Code { get; set; }
        public string? Capital { get; set; }
        public string? Region { get; set; }
        public string? SubRegion { get; set; }
        public int? Population { get; set; }
        public string? Demonym { get; set; }
        public double? Area { get; set; }
        public double? Gini { get; set; }
        public string? NativeName { get; set; }
        public string? NumericCode { get; set; }
        public object? Translations { get; set; }
        public string? Flag { get; set; }
        public string? Cioc { get; set; }
        public List<string> TopLevelDomain { get; set; }
        public List<string> CallingCodes { get; set; }
        public List<string> AltSpellings { get; set; }
        public List<decimal> LatLng { get; set; }
        public List<string> Timezones { get; set; }
        public List<string> Borders { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<RegionalBloc> RegionalBlocs { get; set; }
        public List<Language> Languages { get; set; }

    }

    public class RegionalBloc
    {
        public string Acronym { get; set; }
        public string Name { get; set; }
        public List<string> OtherAcronyms { get; set; }
        public List<string> OtherNames { get; set; }
    }

    public class Currency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }

    public class Language
    {
        public string ISO639_1 { get; set; }
        public string ISO639_2 { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }
    }
}