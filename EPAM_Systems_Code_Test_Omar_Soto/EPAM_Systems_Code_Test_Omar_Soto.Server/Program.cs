using EPAM_Systems_Code_Test_Omar_Soto.Server.Application.TextProcessor;
using EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.TextProcessor;
using EPAM_Systems_Code_Test_Omar_Soto.Server.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITextProcessorService, TextProcessorService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

//Extension Method to add new nugets in the future.
builder.Services.AddInfrastructure();

//Configuring CORS
if (builder.Environment.IsDevelopment())
{
    var allowedOrigins = builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();

    builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins(allowedOrigins ?? [])
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    }));
}

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("CorsPolicy");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

//Extension Method that handles all the SignalR Hubs
app.AddSignalRHubs();

app.Run();
