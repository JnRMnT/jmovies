using JMovies.App.Business.Middlewares;
using Microsoft.AspNetCore.Builder;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseIPRestriction(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<IPRestrictionMiddleware>();
    }
}