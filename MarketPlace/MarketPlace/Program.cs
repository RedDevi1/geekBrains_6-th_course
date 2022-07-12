using MarketPlace.Interfaces;
using MarketPlace.Services;
using MarketPlace.Models;
using Serilog;
using Serilog.Events;
using MarketPlace.BackgroundServices;
using MarketPlace.DomainEvents.EventConsumers;
using Microsoft.AspNetCore.HttpLogging;
using MarketPlace.Middleware;

Log.Logger = new LoggerConfiguration()
   .WriteTo.Console()
   .CreateBootstrapLogger(); //означает, что глобальный логер будет заменен на вариант из Host.UseSerilog
Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews();
    builder.Services.AddSingleton<IGoodsCatalog, GoodsCatalog>();
    builder.Services.Configure<SmtpCredentials>(builder.Configuration.GetSection("SmtpCredentials"));
    builder.Services.AddScoped<IEmailService, MailKitService>();
    builder.Host.UseSerilog((ctx, conf) => conf.ReadFrom.Configuration(ctx.Configuration));
    builder.Services.AddHostedService<ServerRuningEmailSender>();
    builder.Services.AddHostedService<ProductAddedEventHandler>();
    builder.Services.AddHttpLogging(options =>
    {
        options.LoggingFields = HttpLoggingFields.RequestHeaders
        | HttpLoggingFields.ResponseHeaders
        | HttpLoggingFields.RequestBody
        | HttpLoggingFields.ResponseBody;
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    
    app.UseHttpLogging();
    app.UseHttpsRedirection();
    app.UseMiddleware<HttpPagesTraversesCountMiddleware>();
    //app.UseMiddleware<BrowserFilterMiddleware>();
    app.UseStaticFiles();
    app.UseSerilogRequestLogging();
    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    
    app.Run();
}
catch (Exception ex)
{
    if (ex.GetType().Name is "StopTheHostException")
        throw;
    Log.Fatal(ex, "Unhandled exception on server startup");
    throw;
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush(); //перед выходом дожидаемся пока все логи будут записаны
}



