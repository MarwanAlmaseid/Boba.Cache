namespace Boba.Cache;

public interface ICacheKeyService
{
    string PrepareKey(string cacheKey, params object[] cacheKeyParameters);
}