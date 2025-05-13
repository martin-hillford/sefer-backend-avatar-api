using Sefer.Backend.Authentication.Lib;

var builder = WebApplication.CreateBuilder(args);
var app = builder.WithSharedConfig().AddTokenAuthentication().Build();
var client = new HttpClient();

app.MapGet("/avatar", async (HttpContext http, IConfiguration configuration, string hash, string initials, string fill, string color) =>
{
    // The browser is allowed to cache the result of this call for hour
    http.Response.Headers.CacheControl = $"public,max-age={TimeSpan.FromHours(1).TotalSeconds}";

    var support = new Support(configuration);
    return await support.GetAvatar(hash, initials, fill, color);
});

app.MapGet("/avatar-no-cache", async (IConfiguration configuration, string hash, string initials, string fill, string color) =>
{
    var support = new Support(configuration);
    return await support.GetAvatar(hash, initials, fill, color);
});

app.MapPost("/upload", async (HttpContext http, IServiceProvider provider, string hash, string? token, [FromBody] UploadModel body) =>
{
    try
    {
        // Prevent unauthorized uploads
        var hasAccess = Auth.Create(http, provider).IsAuthenticated(token);
        if (!hasAccess) return Results.Unauthorized();
        
        // And save the image to the cache
        var configuration = provider.GetService<IConfiguration>()!;
        var cache = new Cache(configuration);
        var response = Response.FromBase64(body.Image, body.Type);
        await cache.Store(hash, response);
        return Results.Ok();
    }
    catch { return Results.StatusCode(500); }

});

app.MapGet("/gravatar", async (string hash) =>
{
    var external = new External(client);
    var response = await external.Retrieve(hash, 120);
    return Results.Json(response ?? Response.Empty());
});

app.Run();
