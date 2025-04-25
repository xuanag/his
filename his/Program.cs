using his.Models;
using his.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

#region Cache
// Implementing caching in your .NET 8 application using the in-memory caching mechanism provided by ASP.NET Core
// Microsoft.Extensions.Caching.Memory
builder.Services.AddMemoryCache();
#endregion


#region MongoDB configuration
var mongoConfig = "MongoDB";
// Get MongoDB settings from configuration
var mongoSettings = builder.Configuration.GetSection(mongoConfig).Get<MongoSettings>();

// MongoDB connections are generally thread-safe and designed to be reused across multiple requests.
// The IMongoClient and IMongoDatabase instances are usually registered as singletons because they manage connection pooling internally.
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var settings = builder.Configuration.GetSection(mongoConfig).Get<MongoSettings>();
    return new MongoClient(settings.ConnectionString);
});

// Use code above avoid instance client, database
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = builder.Configuration.GetSection(mongoConfig).Get<MongoSettings>();
    return client.GetDatabase(settings.DatabaseName);
});
#endregion

#region My Services

// Register the generic MongoDB repository
builder.Services.AddScoped(typeof(IMongoService<>), typeof(MongoService<>));

// Register the custom collection repository
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAdmissionService, AdmissionService>();
#endregion


// Add services to the container.
builder.Services.AddControllersWithViews();

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
