namespace Sefer.Backend.Avatar.Api;

public class External(HttpClient client, IConfiguration configuration)
{
    private readonly Gravatar _gravatar = new(client, configuration);

    private readonly Libravatar _libravater = new(client, configuration);

    public async Task<Response?> Retrieve(string hash, int size)
    {
        return await _gravatar.Retrieve(hash, size) ??
               await _libravater.Retrieve(hash, size);
    }
}