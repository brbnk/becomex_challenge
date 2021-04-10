using System.Collections.Generic;
using System.Threading.Tasks;
using BecomexChallenge.Domain.Models;
using BecomexChallenge.Domain.DTO;

namespace BecomexChallenge.Domain.Interfaces
{
    public interface IRestCountriesService
    {
        Task<IEnumerable<CountryDTO>> GetByName(string name);
        Task<CountryDTO> GetByCode(string code);
        Task<IEnumerable<CountryDTO>> GetByCurrency(string currency);

        IEnumerable<RouteOrder> GetRoute(RouteDTO route);
    }
}
