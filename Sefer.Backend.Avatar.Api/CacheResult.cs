namespace Sefer.Backend.Avatar.Api;

public class CacheResult
{
    public readonly Response? Response;

    public bool IsCached => Response != null;

    public CacheResult(Response response)
    {
        Response = response;
    }

    private CacheResult() { }

    public static readonly CacheResult NotCached = new();
}