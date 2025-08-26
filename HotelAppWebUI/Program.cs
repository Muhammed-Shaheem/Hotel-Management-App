using HotelAppLibrary.Data;
using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;

var builder = WebApplication.CreateBuilder(args);
SQLitePCL.Batteries.Init();

// Add services to the container.
builder.Services.AddRazorPages();



builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<ISqliteDataAccess, SqliteDataAccess>();
var dbChoice = builder.Configuration.GetValue<string>("DatabaseChoice").ToLower();

if (dbChoice == "sql")
{
    builder.Services.AddTransient<IDatabaseData, SqlData>();

}
else if (dbChoice == "sqlite")
{
    builder.Services.AddTransient<IDatabaseData, SqliteData>();

}
else
{
    builder.Services.AddTransient<IDatabaseData, SqlData>();
}
var app = builder.Build();





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
