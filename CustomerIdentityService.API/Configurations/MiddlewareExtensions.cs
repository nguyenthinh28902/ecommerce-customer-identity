using CustomerIdentityService.API.Middlewares;

namespace CustomerIdentityService.API.Configurations
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalException(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
