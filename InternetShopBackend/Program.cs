using System.Buffers;
using System.IO.Pipelines;
using System.Text;
using InternetShopBackend;
using InternetShopBackend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Account = InternetShopBackend.Models.Account;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var dbPath = "myapp.db";
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddSingleton<ProfileFilter>();
builder.Services.AddSingleton<FilterActionParams>();
builder.Services.AddScoped<CheckAcc>();
builder.Services.AddSingleton<LogResourceFilter>();
builder.Services.AddSingleton<FilterException>();
builder.Services.AddSingleton<ServiceToken>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddSingleton<IPasswordHasher<AccountRequestModel>, PasswordHasher<AccountRequestModel>>();
builder.Services.AddHttpLogging(option =>
    option.LoggingFields = HttpLoggingFields.ResponseHeaders | HttpLoggingFields.RequestHeaders | HttpLoggingFields.RequestBody 
    | HttpLoggingFields.ResponseBody);

builder.Host.UseSerilog((_, conf) => conf.WriteTo.Console());

builder.Services.AddCors();

JwtConfig jwtConfig = builder.Configuration
    .GetSection("JwtConfig")
    .Get<JwtConfig>();
builder.Services.AddSingleton(jwtConfig);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.SigningKeyBytes),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            RequireSignedTokens = true,

            ValidateAudience = true,
            ValidateIssuer = true,
            ValidAudiences = new[] { jwtConfig.Audience },
            ValidIssuer = jwtConfig.Issuer
        };
    });
builder.Services.AddAuthorization();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(policy => policy
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials());

app.MapControllers();
app.UseHttpLogging();
app.UseMiddleware<CheckBrowserMiddleware>();

app.Use(async (HttpContext context, Func<Task> next) =>
    {

        if (context.Request.Headers.UserAgent.Contains("Edge"))
        {
            await context.Response.WriteAsync("Supported only Edge browser!");
        }
        else
        {
            await next();
        }
    }
);

app.MapGet("/", () => "Hello");
app.MapGet("/Head", (HttpContext context) => context.Request.Headers);




app.Run();


async Task<List<string>> GetListOfStringFromPipe(PipeReader reader)
{
    List<string> results = new List<string>();
    while (true)
    {
        ReadResult readResult = await reader.ReadAsync();
        var buffer = readResult.Buffer;

        SequencePosition? position = null;

        do
        {
            position = buffer.PositionOf((byte)'\n');

            if (position != null)
            {
                var readOnlySequence = buffer.Slice(0, position.Value);
                AddStringToList(results, in readOnlySequence);

                buffer = buffer.Slice(buffer.GetPosition(1, position.Value));
            }
        }
        while (position != null);


        if (readResult.IsCompleted && buffer.Length > 0)
            AddStringToList(results, in buffer);

        reader.AdvanceTo(buffer.Start, buffer.End);

        if (readResult.IsCompleted)
            break;
    }

    return results;
}

static void AddStringToList(List<string> results, in ReadOnlySequence<byte> readOnlySequence)
{
    ReadOnlySpan<byte> span = readOnlySequence.IsSingleSegment ? readOnlySequence.First.Span : readOnlySequence.ToArray().AsSpan();
    results.Add(Encoding.UTF8.GetString(span));
}