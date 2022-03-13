using InternetShopBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var dbPath = "myapp.db";
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddCors();

var app = builder.Build();
app.UseCors(policy => policy
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials());

app.MapControllers();

app.MapGet("/", () => "Hello");
//app.MapGet("/Products", async ([FromServices]AppDbContext context) => await context.Products.ToListAsync());
// app.MapPost("/AddProducts", ([FromBody]Product product, [FromServices]AppDbContext context) =>
// {
//     product.Id = context.Products.ToList().Count == 0
//         ? 0
//         : context.Products.ToList().OrderByDescending(i => i.Id).FirstOrDefault().Id + 1;
//
//
//     context.Products.Add(product);
//     context.SaveChanges();
// });

app.Run();