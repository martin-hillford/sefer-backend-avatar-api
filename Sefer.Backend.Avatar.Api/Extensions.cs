namespace Sefer.Backend.Avatar.Api;

public static class Extensions
{
    public static async Task<string> DownloadBlobToStringAsync(this BlobClient blobClient)
    {
        BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();
        return downloadResult.Content.ToString();
    }

    public static IResult Send(this Response? response)
    {
        // http.Response.Headers.CacheControl = "max-age=2592000";
        if (response == null) return Results.NotFound();
        if (!response.HasImage) return Results.NotFound();

        if (!response.IsBase64) return Results.Content(response.Content!, response.ContentType);

        var contents = Convert.FromBase64String(response.Base64!);
        return Results.Bytes(contents, response.ContentType);
    }
}
