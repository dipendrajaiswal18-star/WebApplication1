var builder = WebApplication.CreateBuilder(args);

// Get logger from builder
builder.Services.AddLogging();
// Add services to the container.
//builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Binding default static pages
//DefaultFilesOptions obj = new DefaultFilesOptions();
//obj.DefaultFileNames.Clear();
//obj.DefaultFileNames.Add("MyPage.html");


FileServerOptions objFileServer = new FileServerOptions();
objFileServer.DefaultFilesOptions.DefaultFileNames.Clear();
objFileServer.DefaultFilesOptions.DefaultFileNames.Add("MyPage.html");



//app.UseDefaultFiles(obj);
//app.UseStaticFiles();

//app.UseFileServer(objFileServer);


app.UseHttpsRedirection();
app.UseRouting();

//app.UseAuthorization();

app.MapStaticAssets();

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("a1 start");
    await context.Response.WriteAsync("\ncustom display!");
    await next();
    logger.LogInformation("a1 end");
});

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("a2 start");
    await context.Response.WriteAsync("\ncustom display-2!");
    await next();
    logger.LogInformation("a2 end");
});


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}")
//    .WithStaticAssets();


app.Run(async (context) => {
    await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
});

app.Run();