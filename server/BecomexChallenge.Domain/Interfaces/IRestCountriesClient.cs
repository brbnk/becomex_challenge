using BecomexChallenge.Domain.DTO;
using BecomexChallenge.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecomexChallenge.Domain.Interfaces
{
    public interface IRestCountriesClient
    {
        Task<CountryDTO> Get(string path);
        Task<IEnumerable<CountryDTO>> GetList(string path);
    }
}
