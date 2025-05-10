namespace Sefer.Backend.Avatar.Api;

public class AccessToken(IConfiguration configuration)
{
    // NB. the duration should be slightly higher than the allowed cache duration
    private const int Duration = 3800;

    private readonly string? _apiKey = configuration.GetSection("Avatar").GetValue<string>("ApiKey");

    public bool IsValidToken(string token)
    {
        try
        {
            var parts = token.Split('.');
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var time = long.Parse(parts[0]);

            if (time + Duration < now) return false;

            var expected = CreateToken(time, _apiKey!);
            return expected == token;
        }
        catch (Exception) { return false; }
    }

    public string CreateToken(long unixTime, string apiKey)
    {
        var data = unixTime + apiKey;
        var bytes = Encoding.UTF8.GetBytes(data);
        var hash = System.Security.Cryptography.SHA256.HashData(bytes);
        var hex = BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);
        return $"{unixTime}.{hex}";
    }
}