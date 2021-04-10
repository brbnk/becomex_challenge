using BecomexChallenge.Domain.DTO;
using BecomexChallenge.Domain.External;
using BecomexChallenge.Domain.Interfaces;
using BecomexChallenge.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BecomexChallenge.Business.Services
{
    public class RestCountriesService : IRestCountriesService
    {
        private readonly RestCountriesClient client;
        private ICache _cache;

        public RestCountriesService(ICache cache)
        {
            client = new RestCountriesClient();
            _cache = cache;
        }

        public async Task<CountryDTO> GetByCode(string code)
        {
            var redisKey = $"{code.ToLower().Trim()}";

            var cache = await _cache.GetCachedResult(redisKey);

            if (!string.IsNullOrEmpty(cache))
            {
                var cachedResult = JsonConvert.DeserializeObject<CountryDTO>(cache);

                return cachedResult;
            }

            var response = await client.Get($"alpha/{code}");

            await _cache.SetResultInCache(redisKey, JsonConvert.SerializeObject(response));

            return response;
        }

        public async Task<IEnumerable<CountryDTO>> GetByCurrency(string currency)
        {
            var redisKey = $"{currency.ToLower().Trim()}";

            var cache = await _cache.GetCachedResult(redisKey);


            if (!string.IsNullOrEmpty(cache))
            {
                var cachedResult = JsonConvert.DeserializeObject<IEnumerable<CountryDTO>>(cache);

                return cachedResult;
            }

            var response = await client.GetList($"currency/{currency}");

            await _cache.SetResultInCache(redisKey, JsonConvert.SerializeObject(response));

            return response;
        }

        public async Task<IEnumerable<CountryDTO>> GetByName(string name)
        {
            var redisKey = $"{name.ToLower().Trim()}";

            var cache = await _cache.GetCachedResult(redisKey);

            if (!string.IsNullOrEmpty(cache))
            {
                var cachedResult = JsonConvert.DeserializeObject<IEnumerable<CountryDTO>>(cache);

                return cachedResult;
            }

            var response = await client.GetList($"name/{name}");

            await _cache.SetResultInCache(redisKey, JsonConvert.SerializeObject(response));

            return response;
        }

        public IEnumerable<RouteOrder> GetRoute(RouteDTO route)
        {
            var routes = new List<RouteOrder>();
            var bordersToIgnore = new List<string>();
            var found = false;
            
            var origin = route.From;
            var destination = route.To;

            if (origin.Borders.Count > 0)
            {
                if (origin.Borders.Contains(destination.Alpha3Code))
                {
                    routes.Add(new RouteOrder { Order = 0, Country = origin });
                    routes.Add(new RouteOrder { Order = 1, Country = destination });

                    return routes;
                }

                foreach (var border in origin.Borders)
                {
                    bordersToIgnore = origin.Borders.Where(b => b != border).ToList();
                    bordersToIgnore.Add(origin.Alpha3Code);

                    routes.Add(new RouteOrder { Order = 0, Country = origin });

                    MakeRoute(border, destination, routes, bordersToIgnore, ref found);

                    if (found) break;

                    bordersToIgnore = new List<string>();
                    routes = new List<RouteOrder>();
                }
            }

            return routes;
        }

        private void MakeRoute(string border, CountryDTO destination, List<RouteOrder> route, List<string> borderToIgnore, ref bool found)
        {
            if (found) return;

            var redisKey = $"{border.ToLower().Trim()}";

            var cache = _cache.GetCachedResult(redisKey).Result;

            var country = new CountryDTO();

            if (!string.IsNullOrEmpty(cache))
            {
                country = JsonConvert.DeserializeObject<CountryDTO>(cache);
            }
            else
            {
                country = client.Get($"alpha/{border}").Result;

                _cache.SetResultInCache(redisKey, JsonConvert.SerializeObject(country));
            }

            if (border == destination.Alpha3Code)
            {
                route.Add(new RouteOrder { Order = route.Count + 1, Country = country });
                found = true;
                return;
            }

            var potentialRoute = false;

            foreach (var borderCountry in country.Borders)
            {
                if (!borderToIgnore.Contains(borderCountry))
                {
                    potentialRoute = true;
                    break;
                }
            }

            if (potentialRoute)
                route.Add(new RouteOrder { Order = route.Count, Country = country });

            foreach (var subBorder in country.Borders)
            {
                if (borderToIgnore.Contains(subBorder))
                    continue;

                borderToIgnore.Add(subBorder);
                borderToIgnore.Add(border);
                
                // Recursion
                MakeRoute(subBorder, destination, route, borderToIgnore, ref found);
            }
        }
    }
}
