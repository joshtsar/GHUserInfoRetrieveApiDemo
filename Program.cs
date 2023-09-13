using GitHubUsersInfoDemoByJiahuaTong.Middleware;
using GitHubUsersInfoDemoByJiahuaTong.Service;
using GitHubUsersInfoDemoByJiahuaTong.Service.Interfaces;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration config = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

Log.Logger = new LoggerConfiguration()
       .ReadFrom.Configuration(config)
      .CreateLogger();

builder.Services.AddLogging(configure => configure.AddSerilog());

builder.Services.AddSingleton(config);

builder.Services.AddScoped<IGHPublicApi ,GHPublicAPIService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Github Public API-Get User Info REST API Demo",
        Version = "v1"
    });
});

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
else
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlerMiddleware>();
//app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
