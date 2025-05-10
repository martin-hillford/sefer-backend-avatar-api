namespace Sefer.Backend.Avatar.Api;

public class Gravatar
{
    private readonly HttpClient _client;

    private readonly Support _support;

    public Gravatar(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
        _support = new Support(configuration);
    }

    public async Task<Response?> Retrieve(string hash, int size)
    {
        try
        {
            var uri = $"https://www.gravatar.com/{hash}.json";
            var response = await _client.GetStringAsync(uri);
            var json = JsonNode.Parse(response);
            var result = json?["entry"]?[0]?["thumbnailUrl"];
            if (result == null) return null;

            var imageUri = $"{result}?size={size}";
            return await _support.DownloadImageAsync(_client, imageUri);
        }
        catch (Exception)
        {
            return null;
        }
    }
}