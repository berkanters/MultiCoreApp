 using Microsoft.EntityFrameworkCore;
 using MultiCoreApp.API.Filters;
 using MultiCoreApp.Core.IntRepository;
 using MultiCoreApp.Core.IntService;
 using MultiCoreApp.Core.IntUnitOfWork;
 using MultiCoreApp.DataAccessLayer;
 using MultiCoreApp.DataAccessLayer.Repository;
 using MultiCoreApp.DataAccessLayer.UnitOfWork;
 using MultiCoreApp.MVC.ApiServices;
 using MultiCoreApp.Service.Services;

 var builder = WebApplication.CreateBuilder(args);
 builder.Services.AddHttpClient<CategoryApiService>(options => options.BaseAddress = new Uri(builder.Configuration["BaseUrl"]));
 builder.Services.AddHttpClient<ProductApiService>(options => options.BaseAddress = new Uri(builder.Configuration["BaseUrl"]));
// Add services to the container.
builder.Services.AddControllersWithViews();
 builder.Services.AddAutoMapper(typeof(Program));
 builder.Services.AddScoped<CategoryNotFoundFilter>();
//iliþkili kodun yaþam süreci
// Add services to the container.
 builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
 builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
 builder.Services.AddScoped<ICategoryService, CategoryService>();
 builder.Services.AddScoped<IProductService, ProductService>();
 builder.Services.AddScoped<ICustomerService, CustomerService>();
 builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
 
 builder.Services.AddDbContext<MultiDbContext>(options =>
 {
     options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCOnStr"), sqlServerOptionsAction: sqlOptions =>
     {
         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
         sqlOptions.MigrationsAssembly("MultiCoreApp.DataAccessLayer");
     });
 });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
