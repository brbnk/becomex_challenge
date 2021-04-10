using System.Threading.Tasks;

namespace BecomexChallenge.Domain.Interfaces
{
    public interface ICache
    {
        Task<string> GetCachedResult(string key);
        Task SetResultInCache(string key, string result);
    }
}
