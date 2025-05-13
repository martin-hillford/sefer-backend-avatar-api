using Sefer.Backend.Authentication.Lib;

namespace Sefer.Backend.Avatar.Api;

internal class Auth(HttpContext context, IServiceProvider provider)
{
    internal bool IsAuthenticated(string? token)
    {
        var configuration = provider.GetService<IConfiguration>();
        var tokenGenerator = provider.GetService<ITokenGenerator>();
        if (tokenGenerator == null || configuration == null) return false; 
        
        
        var hasAccess = AccessToken.Create(configuration).IsValidToken(token ?? string.Empty);
        if(hasAccess) return true;
        
        var tokenProvider = new TokenAuthenticationProvider(context.Request,tokenGenerator);
        return tokenProvider.IsAuthenticated();
    }
    
    internal static Auth Create(HttpContext context, IServiceProvider provider) => new(context, provider); 
}