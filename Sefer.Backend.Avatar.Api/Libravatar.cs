namespace Sefer.Backend.Avatar.Api;

public class Libravatar
{
    private readonly HttpClient _client;

    private readonly Support _support;

    public Libravatar(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
        _support = new(configuration);
    }

    public async Task<Response?> Retrieve(string hash, int size)
    {
        try
        {
            var uri = $"https://seccdn.libravatar.org/avatar/{hash}?s={size}&forcedefault=y&default=404";
            return await _support.DownloadImageAsync(_client, uri);
        }
        catch (Exception)
        {
            return null;
        }
    }
}


