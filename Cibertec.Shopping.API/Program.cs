using Cibertec.Shopping.API.Middleware;
using Cibertec.Shopping.CORE.Interfaces;
using Cibertec.Shopping.CORE.Services;
using Cibertec.Shopping.INFRASTRUCTURE.Data;
using Cibertec.Shopping.INFRASTRUCTURE.Repositories;
using Cibertec.Shopping.INFRASTRUCTURE.Shared;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Add Logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo
    .File("Logs/CibertecShoppingLog.txt",
    rollingInterval: RollingInterval.Minute).CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
var _config = builder.Configuration;
var connectionString = _config.GetConnectionString("DevConnection");

builder.Services.AddDbContext<StoreDbcibertecContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>(); 
builder.Services.AddTransient<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddTransient<IFavoriteService, FavoriteService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddSharedInfrastructure(_config);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//Add Middleware
app.UseMiddleware<JsonWrapperMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
