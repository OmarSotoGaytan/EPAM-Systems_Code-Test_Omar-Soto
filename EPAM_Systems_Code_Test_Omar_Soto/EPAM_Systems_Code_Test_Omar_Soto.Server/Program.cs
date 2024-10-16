using EPAM_Systems_Code_Test_Omar_Soto.Server.Application.TextProcessor;
using EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.TextProcessor;
using EPAM_Systems_Code_Test_Omar_Soto.Server.Infrastructure.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ITextProcessorService, TextProcessorService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSignalR();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.MapHub<TextProcessorHub>("/process");

app.Run();
