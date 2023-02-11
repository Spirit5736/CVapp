using API.models;
using API.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => //CookieAuthenticationOptions
                    {
                        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    });
            services.AddDbContext<UserContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}