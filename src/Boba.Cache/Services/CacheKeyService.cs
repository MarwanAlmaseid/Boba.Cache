using System.Text;

namespace Boba.Cache;

public class CacheKeyService : ICacheKeyService
{
    public string PrepareKey(string cacheKey, params object[] cacheKeyParameters)
    {
        const char seperator = '-';
        var key = new StringBuilder(string.Join(seperator, cacheKey));

        if (cacheKeyParameters.Any())
        {
            key.Append(seperator);
            key.AppendJoin(seperator, cacheKeyParameters);
        }

        return key.ToString();
    }
}