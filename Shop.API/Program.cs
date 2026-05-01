using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Repositories;
using Shop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ShopDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// .csproj reikalinga <PackageReference Include="Swashbuckle.AspNetCore.SwaggerUI" Version="10.1.7" /> 
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapControllers();
app.Run();


