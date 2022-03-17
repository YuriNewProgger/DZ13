using InternetShopBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Account = InternetShopBackend.Models.Account;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var dbPath = "myapp.db";
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton<IPasswordHasher<AccountRequestModel>, PasswordHasher<AccountRequestModel>>();

builder.Services.AddCors();

var app = builder.Build();
app.UseCors(policy => policy
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials());

app.MapControllers();

app.MapGet("/", () => "Hello");

app.Run();