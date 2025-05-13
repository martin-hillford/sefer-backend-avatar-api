namespace Sefer.Backend.Avatar.Api;

public class Gravatar
{
    private readonly HttpClient _client;

    public Gravatar(HttpClient client)
    {
        _client = client;
        _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
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
            return await Support.DownloadImageAsync(_client, imageUri);
        }
        catch (Exception)
        {
            return null;
        }
    }
}