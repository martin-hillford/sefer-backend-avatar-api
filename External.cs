namespace Sefer.Backend.Avatar.Api;

public class External(HttpClient client)
{
    private readonly Gravatar _gravatar = new(client);

    private readonly Libravatar _libravater = new(client);

    public async Task<Response?> Retrieve(string hash, int size)
    {
        return await _gravatar.Retrieve(hash, size) ??
               await _libravater.Retrieve(hash, size);
    }
}