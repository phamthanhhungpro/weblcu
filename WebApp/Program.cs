using Datas;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services;
using System.Configuration;
using System.Globalization;
using WebApp.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddDbContext<DataContext>();
builder.Services.AddDbContext<DataContext>(b => b.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("db")));
//builder.Services.AddDbContext<DataContext>(b => b.UseSqlServer(
//                @"Data Source=(local);Initial Catalog=LCU;Persist Security Info=True;User ID=sa;Password=admin@123;TrustServerCertificate=True;"));
builder.Services.AddTransient<DataInitializer>();
builder.Services.AddScoped<AuthenUtils>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<CompanyService>();
builder.Services.AddTransient<GroupFunctionService>();
builder.Services.AddTransient<DepartmentService>();
builder.Services.AddTransient<PositionService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<RoleService>();
builder.Services.AddTransient<FunctionService>();
builder.Services.AddTransient<NewsCategoryService>();
builder.Services.AddTransient<NewsService>();
builder.Services.AddTransient<PeopleCategoryService>();
builder.Services.AddTransient<PeopleService>();
builder.Services.AddTransient<NationalCostumeCategoryService>();
builder.Services.AddTransient<NationalCostumeService>();
builder.Services.AddTransient<Image360Service>();
builder.Services.AddTransient<ContactService>();
builder.Services.AddTransient<WardService>();
builder.Services.AddTransient<DistrictService>();
builder.Services.AddTransient<LanguageService>();
builder.Services.AddTransient<FileService>();
builder.Services.AddTransient<LogService>();

builder.Services.AddTransient<InstrumentCategoryService>();
builder.Services.AddTransient<InstrumentService>();

builder.Services.AddSingleton<AdminSettingUtils>();
builder.Services.AddSingleton<SettingUtils>();
builder.Services.AddSingleton<MailUtils>();
builder.Services.AddSingleton<ViewFileUtils>();

builder.Services.AddOutputCache(options =>
{
    options.AddPolicy("10Minute", builder =>
        builder.Expire(TimeSpan.FromMinutes(10)));
    options.AddPolicy("20Minute", builder =>
        builder.Expire(TimeSpan.FromMinutes(20)));
    options.AddPolicy("30Minute", builder =>
        builder.Expire(TimeSpan.FromMinutes(30)));
    options.AddPolicy("60Minute", builder =>
        builder.Expire(TimeSpan.FromMinutes(60)));
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = int.MaxValue;
});

builder.Services.AddSession();
var app = builder.Build();
using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var initialiser = services.GetRequiredService<DataInitializer>();

initialiser.Run();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

StaticFileOptions options = new StaticFileOptions { ContentTypeProvider = new FileExtensionContentTypeProvider() };
((FileExtensionContentTypeProvider)options.ContentTypeProvider).Mappings.Add(new KeyValuePair<string, string>(".glb", "model/gltf-buffer"));
app.UseStaticFiles(options);

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Default}/{action=Index}/{id?}");

var supportedCultures = new[] { new CultureInfo("en-GB") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-GB"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

//app.UseEndpoints(endpoints =>
//{
//    _ = endpoints.MapAreaControllerRoute(
//       name: "Admin",
//       areaName: "Admin",
//       pattern: "Admin/{controller=Default}/{action=Index}/{id?}"
//   );

//});

//app.MapAreaControllerRoute(
//    name: "admin",
//    areaName: "Admin",
//    pattern: "Admin/{controller=Default}/{action=Index}/{id?}");

app.Run();
