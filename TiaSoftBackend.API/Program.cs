using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using TiaSoftBackend.API;
using TiaSoftBackend.API.Hubs;
using TiaSoftBackend.Data;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.UseCases;

var builder = WebApplication.CreateBuilder(args);

var authenticatedUserPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    optionsBuilder.UseMySQL(connectionString);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // No automatic redirection for unauthorized access.
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401; // Unauthorized
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = 403; // Forbidden
            return Task.CompletedTask;
        };
    });

builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter(authenticatedUserPolicy));
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddSignalR();

builder.Services
    .AddUseCases()
    .AddMappers()
    .AddData();

builder.Services.AddTransient<DataSeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure CORS to allow credentials and specific origins
app.UseCors(builder =>
    builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .WithMethods("GET", "POST", "PUT", "DELETE")
        .AllowCredentials());

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await using (var scope = app.Services.CreateAsyncScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await seeder.Seed();
}

// ------- SIGNALR HUBS -------
app.MapHub<TableHub>("api/hubs/table");
app.MapHub<OrderHub>("api/hubs/order");

app.Run();
