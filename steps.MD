# API setup

1. ### Creating Project

   ###### ASP.NET Core Web API

2. ### NuGet packages

   ```
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.4" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="10.1.7" />
   ```
3. ### Create Controllers folder
4. ### Create classes, e.g. WeatherController : ControllerBase
5. ### Add Services setup to Program.cs
    ```
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<ShopDbContext>(options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });

    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserService, UserService>();

    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapControllers();
    app.Run();
    ```
6. ### Add ConnectionStrings to appsettings.json
    ```
    "ConnectionStrings": {
        "DefaultConnection": "Server=127.0.0.1;Port=3306;Database=shop;Uid=root;Pwd=root;"
    },
    ```
7. ### Add Project references, e.g. Shop.API < Shop.Entities, Shop.Services
8. ### Add Usings to Program.cs

# EF Core setup for Console app
1. ### Docker setup
    ```
    docker run --name some-mysql -e MYSQL_ROOT_PASSWORD=my-secret-pw -d -p 3306:3306 mysql:lts
    ```
2. ### Create New Schema in Workbench
3. ### Install global Entity Framework
    ```
    dotnet tool install --global dotnet-ef
    ```
4. ### Create new Class Library, e.g. Shop.Data
5. ### Create new class, e.g. ShopDbContext.cs, implementing DbContext interface
    ```
    using Microsoft.EntityFrameworkCore;
    using Shop.Entities;

    namespace Shop.Data
    {
        public class ShopDbContext : DbContext
        {
            public DbSet<Product> Products { get; set; }
            public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
            {

            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Product>(o=>
                {
                    o.HasKey(e => e.Id);
                    o.Property(e=>e.Name).IsRequired();
                    o.Property(e =>e.Price).IsRequired();
                });
            }
        }
    }
    ```
6. ### Add NuGet packages !!! Versions must match !!!
    ```
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.0">
    <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="9.0.0" />
    ```
7. ### Add Connection Striong to appsettings.json
    ```
    "ConnectionStrings": {
        "DefaultConnection": "Server=127.0.0.1;Port=3306;Database=shop;Uid=root;Pwd=root;"
    },
    ```
8. ### Setup appsettings.json to be always copyable on build. Edit *.csproj
    ```
    <ItemGroup>
        <None Update="appsettings.json">
        <CopyToOutputDirectory>Always</CopyToOutputDirectory>
        </None>
    </ItemGroup>
    ```
9. ### Add NuGet to Console project
    ```
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.0">
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="10.0.7" />
    ```
10. ### Add dbContext and BuildHost() method to Program.cs
    ```
    static void Main(string[] args)
        {
            var host = BuildHost();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var dbContext = serviceProvider.GetRequiredService<ShopDbContext>();
                dbContext.Database.Migrate();

                var productService = serviceProvider.GetRequiredService<IProductService>();

                // var id = productService.Add(new Product()
                // {
                //     Name = "Book",
                //     Price = 25.99M
                // });

                // var product = productService.Get(id);
                // Console.WriteLine($"Product {product.Name}");
            }

        }
        public static IHost BuildHost()
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices(
                (context, services) =>
            {
                //takes DefaultConnection parameter from appsettings.json
                var connectionString = context.Configuration
                    .GetConnectionString("DefaultConnection");

                services.AddDbContext<ShopDbContext>(options =>
                {
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                });

                services.AddScoped<IProductRepository, ProductRepository>();
                services.AddScoped<IProductService, ProductService>();
            });
            return host.Build();
        }
    ```
11. ### Add reference Shop.Console < Shop.Data
12. ### Generate db migration files in Data project
    ```
    dotnet ef migration add InitialCreate -p Shop.Data -s Shop.Console
    ```