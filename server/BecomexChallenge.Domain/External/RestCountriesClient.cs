using BecomexChallenge.Domain.DTO;
using BecomexChallenge.Domain.Interfaces;
using BecomexChallenge.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BecomexChallenge.Domain.External
{
    public class RestCountriesClient : IRestCountriesClient
    {
        private readonly string _baseurl = "https://restcountries.eu/rest/v2";

        public async Task<CountryDTO> Get(string path)
        { 
            using var client = new HttpClient();

            var response = await client.GetAsync($"{_baseurl}/{path}");

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CountryDTO>(content);
        }

        public async Task<IEnumerable<CountryDTO>> GetList(string path)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"{_baseurl}/{path}");

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<CountryDTO>>(content);
        }
    }
}
