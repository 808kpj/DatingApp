using System.Text;
using API;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddIdentityServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200" , "https://localhost:4200"));

//ask if you have a valid token
app.UseAuthentication();

//ask what are you allow to do with the token
app.UseAuthorization();
app.MapControllers();

//must create a disposable scope variable to inject into the program file
using var scope = app.Services.CreateScope();

//assign the scope service provider to a variable, will be used to call any service needed
var services = scope.ServiceProvider;

try
{
    //us the variable to call the service needed, in this example data context
    var contexts = services.GetRequiredService<DataContext>();

    //perform your action needed
    //This will automatically perform updates/migrations to the database when changes are made
    await contexts.Database.MigrateAsync();

    //then will apply a password hash and salt then will lowercase the username before adding to database
    await Seed.SeedUsers(contexts);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
