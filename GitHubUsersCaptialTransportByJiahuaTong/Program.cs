using Serilog;
using System.Reflection;

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


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Github Public API-Capital Transport Test",
        Version = "v1"
    });
});


builder.Services.AddHttpClient();

var app = builder.Build();

app.UseExceptionHandler("/error/401");
app.UseExceptionHandler("/error/403");
app.UseExceptionHandler("/error/500");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseDeveloperExceptionPage();
}
//app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
