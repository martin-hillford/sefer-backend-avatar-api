// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Sefer.Backend.Avatar.Api;

public class UploadModel
{
    [Required, MinLength(1)]
    public string? Image { get; set; }

    [Required, MinLength(1)]
    public string? Type { get; set; }
}