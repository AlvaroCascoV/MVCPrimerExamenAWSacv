using Amazon.S3;
using ExamenAWSZapatillas.Helpers;
using ExamenAWSZapatillas.Models;
using Microsoft.EntityFrameworkCore;
using MVCPrimerExamenAWSacv.Data;
using MVCPrimerExamenAWSacv.Repositories;
using MVCPrimerExamenAWSacv.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//string connectionString = builder.Configuration.GetConnectionString("AWSMySQL");


builder.Services.AddTransient<RepositoryZapatillas>();
builder.Services.AddTransient<ServiceStorageS3>();
builder.Services.AddAWSService<IAmazonS3>(new Amazon.Extensions.NETCore.Setup.AWSOptions
{
    Region = Amazon.RegionEndpoint.USEast2
});


string jsonSecret = await HelperSecretManager.GetSecretAsync();

// Deserializamos el JSON usando tu clase KeysModel
KeysModel keys = JsonSerializer.Deserialize<KeysModel>(jsonSecret, new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
});
builder.Services.AddSingleton(keys);


string connectionString = keys.AWSMySQL;
builder.Services.AddDbContext<ZapatillasContext>(options =>
    options.UseMySQL(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
