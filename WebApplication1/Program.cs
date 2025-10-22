using Microsoft.AspNetCore.Http;
using WebApplication1;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

var config= builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

//configue DI for GuidService
//builder.Services.AddSingleton<IGuidService, GuidService>();
builder.Services.AddSingleton<IGuidService, GuidService2>();
//builder.Services.AddTransient<IGuidService, GuidService>();
//builder.Services.AddScoped<IGuidService, GuidService>();

var app = builder.Build();
//builder.Services.AddLogging();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
//defaultFilesOptions.DefaultFileNames.Clear();
//defaultFilesOptions.DefaultFileNames.Add("MyPage.html");

//app.UseDefaultFiles(defaultFilesOptions);
//app.UseStaticFiles();

//FileServerOptions fileServerOptions = new FileServerOptions();
//fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
//fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("MyPage.html");

//app.UseFileServer(fileServerOptions);

//app.UseMiddleware<RequestTracingMiddleware>();

app.UseRequestTracingMiddleware();

app.UseHttpsRedirection();
app.UseRouting();

//app.Use(async (context, next) => {
//    await context.Response.WriteAsync("Start -1");
//    await context.Response.WriteAsync("Hello from 1st delegate.");
//    await next();
//    await context.Response.WriteAsync("end -1");
//});

//app.Use(async (context, next) => {
//    await context.Response.WriteAsync("Start -2");
//    await context.Response.WriteAsync("Hello from 2nd delegate.");
//    await next();
//    await context.Response.WriteAsync("end -2");
//});

//app.Map("/api", apiapp => { 
//    apiapp.Use(async (context, next) => {
//        await context.Response.WriteAsync("Start - use -1");
//        await context.Response.WriteAsync("Custom route -1");
//        await next();
//        await context.Response.WriteAsync("end -use -1");
//    });
//    apiapp.Run(async (context) => {
//        await context.Response.WriteAsync(" end run - 2");
//    });
//});

//app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

//app.Run(async (context) => {
//    var logger = app.Services.GetRequiredService<ILogger<Program>>();
//    logger.LogInformation("logging test ");
//    //await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
//    await context.Response.WriteAsync(config["myconfig"]);
//});

app.Run();