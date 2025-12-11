namespace gesn.webApp.Infrastructure.Middleware
{
    public static class LoginRateLimitingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginRateLimiting(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginRateLimitingMiddleware>();
        }
    }
}
