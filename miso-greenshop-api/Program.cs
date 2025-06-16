using miso_greenshop_api.Application.Models;
using miso_greenshop_api.Application.Profiles;
using miso_greenshop_api.Domain.Interfaces.Creators;
using miso_greenshop_api.Domain.Interfaces.Jwt;
using miso_greenshop_api.Domain.Interfaces.Modules;
using miso_greenshop_api.Domain.Interfaces.Repositories;
using miso_greenshop_api.Domain.Interfaces.Service;
using miso_greenshop_api.Domain.Interfaces.Services;
using miso_greenshop_api.Infrastructure.Bootstrap;
using miso_greenshop_api.Infrastructure.Creators;
using miso_greenshop_api.Infrastructure.Newsletter;
using miso_greenshop_api.Infrastructure.Persistance;
using miso_greenshop_api.Infrastructure.Repositories;
using miso_greenshop_api.Infrastructure.Services;
using miso_greenshop_api.Infrastructure.Services.Newsletter;
using MediatR;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.RateLimiting;
using miso_greenshop_api.Filters.ExceptionFilters.General;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication
    .CreateBuilder(args);

var connectionString = builder.Configuration
    .GetConnectionString("MisoGreenshopManagement");

builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseNpgsql(connectionString);
    });

builder.Services
    .AddTransient<NewsletterService>();

builder.Services
    .AddMvc();

builder.Services
    .AddCors(options =>
    {
    options.AddPolicy("DefaultPolicy", policy =>
        policy.WithOrigins(
            "https://calm-bublanina-ac79f6.netlify.app",
            "https://miso-greenshop-api.onrender.com")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
    });

builder.Services
    .AddAutoMapper(
    typeof(CartProfile), 
    typeof(CartItemProfile));

builder.Services
    .AddHttpContextAccessor();

builder.Services
    .AddControllers(options =>
    {
        options.Filters
            .Add(typeof(HandleServerErrorExceptionFilter));
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = 
        ReferenceLoopHandling.Ignore;
        options.SerializerSettings.PreserveReferencesHandling = 
        PreserveReferencesHandling.None;
        options.SerializerSettings.Formatting = 
        Formatting.None;
    });

builder.Services
    .AddRateLimiter(options =>
{
    options.RejectionStatusCode = 
    StatusCodes.Status429TooManyRequests;

    options.AddPolicy("SlidingWindowIpAddressLimiter", httpContext =>
        RateLimitPartition.GetSlidingWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?
            .ToString(),
            factory: _ => new SlidingWindowRateLimiterOptions
            {
                PermitLimit = 120,
                Window = TimeSpan.FromMinutes(1),
                SegmentsPerWindow = 6,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 10
            }
        )
    );

    options.AddPolicy("SlidingWindowIpAddressRestrictLimiter", httpContext =>
        RateLimitPartition.GetSlidingWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?
            .ToString(),
            factory: _ => new SlidingWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(1),
                SegmentsPerWindow = 3,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 5
            }
        )
    );

    options.AddPolicy("TokenBucketIpAddressLimiter", httpContext =>
        RateLimitPartition.GetTokenBucketLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?
            .ToString(),
            factory: _ => new TokenBucketRateLimiterOptions
            {
                TokenLimit = 20,
                TokensPerPeriod = 1,
                ReplenishmentPeriod = TimeSpan.FromSeconds(5),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }
        )
    );

    options.AddPolicy("TokenBucketIpAddressRestrictLimiter", httpContext =>
        RateLimitPartition.GetTokenBucketLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?
            .ToString(),
            factory: _ => new TokenBucketRateLimiterOptions
            {
                TokenLimit = 10,
                TokensPerPeriod = 1,
                ReplenishmentPeriod = TimeSpan.FromSeconds(10),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 2
            }
        )
    );

    options.AddPolicy("ConcurrencyIpAddressLimiter", httpContext =>
        RateLimitPartition.GetConcurrencyLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?
            .ToString(),
            factory: _ => new ConcurrencyLimiterOptions
            {
                PermitLimit = 5,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 10
            }
        )
    );
});

builder.Services
    .Configure<PermissionControlOptions>(builder.Configuration
    .GetSection("PermissionControl"));
builder.Services
    .Configure<JwtOptions>(builder.Configuration
    .GetSection("Jwt"));
builder.Services
    .Configure<SmtpOptions>(builder.Configuration
    .GetSection("Smtp"));

builder.Services
    .AddSmtpClient();

builder.Services
    .AddScoped<IPermissionControlService, PermissionControlService>();

builder.Services
    .AddScoped<IJwtService, JwtService>();

builder.Services
    .AddScoped<INewsletterService, NewsletterService>();

builder.Services
    .AddScoped<IPlantsRepository, PlantsRepository>();
builder.Services
    .AddScoped<IUsersRepository, UsersRepository>();
builder.Services
    .AddScoped<ISubscribersRepository, SubscribersRepository>();
builder.Services
    .AddScoped<IReviewsRepository, ReviewsRepository>();
builder.Services
    .AddScoped<ICartsRepository, CartsRepository>();
builder.Services
    .AddScoped<ICartItemsRepository, CartItemsRepository>();

builder.Services
    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
        Assembly.GetExecutingAssembly()));

builder.Services
    .AddScoped<INewsletterContent, NewsletterContent>();
builder.Services
    .AddScoped<INewsletterCreator, RegistrationNewsletterCreator>();
builder.Services
    .AddScoped<INewsletterCreator, NewPlantNewsletterCreator>();
builder.Services
    .AddScoped<INewsletterCreator, SubscriptionNewsletterCreator>();
builder.Services
    .AddScoped<INewsletterCreator, PurchaseNewsletterCreator>();

builder.Services
    .AddScoped<IActionErrorCreator, ActionErrorCreator>();
builder.Services
    .AddScoped<IExceptionCreator, ExceptionCreator>();

builder.Services
    .AddEndpointsApiExplorer();

builder.Services
    .AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseRouting();

app.UseCors("DefaultPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers();

app.Run();