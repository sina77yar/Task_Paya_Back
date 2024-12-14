using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskPaya_Back.Application.Repositories;
using TaskPaya_Back.Application.Repositories.Interfaces;
using TaskPaya_Back.Persistence.Repositories;
using TaskPaya_Back.Persistence.Repositories.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//register DbContext
//string connectionString = builder.Configuration.GetConnectionString("Development");
string connectionString = builder.Configuration.GetConnectionString("Production");


builder.Services.AddDbContext<PayaTaskDataContext>(options =>
{
    options.UseSqlServer(connectionString).EnableSensitiveDataLogging();
});
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builders =>
    {
        builders.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
         .SetIsOriginAllowed(hostName => true).Build();
    });
});
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("EnableCORS");
app.Run();
