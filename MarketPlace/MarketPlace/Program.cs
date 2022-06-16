using MarketPlace.Interfaces;
using MarketPlace.Services;
using MarketPlace.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IGoodsCatalog, GoodsCatalog>();
builder.Services.Configure<SmtpCredentials>(builder.Configuration.GetSection("SmtpCredentials"));
builder.Services.AddScoped<IEmailService, MailKitService>();
builder.Host.UseSerilog((_, conf) => conf.WriteTo.Console());

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
