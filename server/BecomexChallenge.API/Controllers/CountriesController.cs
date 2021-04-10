using BecomexChallenge.Domain.DTO;
using BecomexChallenge.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BecomexChallenge.Api.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private IRestCountriesService _restCountriesService;

        public CountriesController(IRestCountriesService restCountriesService)
        {
            _restCountriesService = restCountriesService;
        }

        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {

            var result = await _restCountriesService.GetByCode(code);

            return Ok(result);
        }

        [HttpGet("currency/{currency}")]
        public async Task<IActionResult> GetByCurrency(string currency)
        {
            var result = await _restCountriesService.GetByCurrency(currency);

            return Ok(result);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _restCountriesService.GetByName(name);

            return Ok(result);
        }

        [HttpPost("route")]
        public IActionResult GetRouteBetweenTwoCountries([FromBody] RouteDTO body)
        {
            var route = _restCountriesService.GetRoute(body);

            return Ok(route);
        }
    }
}
