namespace Sefer.Backend.Avatar.Api;

public class Support(IConfiguration configuration)
{
    private readonly Cache _cache = new(configuration);

    public async Task<IResult> GetAvatar(string hash, string initials, string fill, string color)
    {
        // Check if an avatar is present in the cache
        var cached = await _cache.FromCache(hash);
        if (cached.IsCached && cached.Response!.HasImage) return cached.Response.Send();

        // Create a fallback avatar
        var fallback = Unknown.Create(initials, fill, color);
        return fallback.Send();
    }

    public static async Task<Response?> DownloadImageAsync(HttpClient client, string imageUri)
    {
        try
        {
            // Check if the image can be downloaded
            var response = await client.GetAsync(imageUri);
            if (!response.IsSuccessStatusCode) return null;

            // Check if the result from the server is an image
            var contentType = response.Content.Headers.ContentType;
            if (contentType?.MediaType == null || !contentType.MediaType!.Contains("image")) return null;

            // Return the result
            var data = await response.Content.ReadAsByteArrayAsync();
            var image = Convert.ToBase64String(data);
            return Response.FromBase64(image, contentType.MediaType, DateTime.UtcNow.AddDays(1));
        }
        catch (Exception)
        {
            return null;
        }
    }
}