using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiCoreApp.API.Extensions;
using MultiCoreApp.API.Filters;
using MultiCoreApp.Core.IntRepository;
using MultiCoreApp.Core.IntService;
using MultiCoreApp.Core.IntUnitOfWork;
using MultiCoreApp.DataAccessLayer;
using MultiCoreApp.DataAccessLayer.Repository;
using MultiCoreApp.DataAccessLayer.UnitOfWork;
using MultiCoreApp.Service.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<CategoryNotFoundFilter>();
//ili?kili kodun ya?am s?reci
// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<ICategoryService ,CategoryService>();
builder.Services.AddScoped<IProductService ,ProductService>();
builder.Services.AddScoped<ICustomerService ,CustomerService>();
builder.Services.AddScoped< IUnitOfWork, UnitOfWork >();
//builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
//builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));


builder.Services.AddDbContext<MultiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCOnStr"), sqlServerOptionsAction: sqlOptions =>
    {
         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        sqlOptions.MigrationsAssembly("MultiCoreApp.DataAccessLayer");
    });
});
builder.Services.AddControllers(o =>
{
    o.Filters.Add(new ValidationFilter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCustomExeption();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
