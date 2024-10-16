using System.Text;
using System.Threading.RateLimiting;
using BetaCustomers.API.Config;
using BetaCustomers.API.IServices;
using BetaCustomers.API.Middleware;
using BetaCustomers.API.Middlewares;
using BetaCustomers.API.Models;
using BetaCustomers.API.Services;
using BetaCustomers.API.Utils;
using BetaCustomers.API.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

// Add services to the container.

//adding serilog as our default logger and telling
//it to fetch the serilog configuration from appsettings.json
builder.Host.UseSerilog((context, config) => 
    config.ReadFrom.Configuration(context.Configuration));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddHealthChecks();
builder.Services.AddTransient<ExceptionHandlerMiddleware>();

builder.Services.AddAuthentication(cfg => {
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration.GetSection("UsersApiOptions:SecretKey").Value) 
        ),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter("global", _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 100,
            Window = TimeSpan.FromMinutes(1),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0
        }));
});
builder.Services.AddMemoryCache();

var app = builder.Build();
app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseJwtMiddleware();
app.UseApiMiddleware();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<RequestLoggerMiddleware>();
app.UseAuthentication();
app.UseSerilogRequestLogging();
app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.Configure<UsersApiConfig>(builder.Configuration.GetSection("UsersApiOptions"));
    services.AddHttpClient<ApiService>();
    services.AddTransient<IUsersService, UsersService>();
    services.AddHttpClient<IUsersService, UsersService>();
    services.AddTransient<IPersonsService, PersonsService>();
    services.AddTransient<IPlaylistService, PlaylistService>();
    services.AddTransient<ILoginService, LoginService>();
    services.AddTransient<IAuthService, AuthService>();
    services.AddScoped<ICacheService, CacheService>();
    services.AddScoped<IValidator<UserModel>, UsersValidator>();
}
