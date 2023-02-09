using System.Text;
using API.models;
using API.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//builder.Services.AddDbContext<APIContext>(options =>
//    options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<APIUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<APIContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => //CookieAuthenticationOptions
        {
            options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        });
builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddCors();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding
                .UTF8.GetBytes(builder.Configuration["TokenKey"])),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        }
        );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();



app.MapControllers();

app.Run();
