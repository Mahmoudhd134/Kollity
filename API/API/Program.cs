using API.Extensions;
using API.Helpers;
using Application;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var connectionString = builder.Configuration["ConnectionStrings:Default"];
builder.Services
    .AddApplicationConfiguration()
    .AddPersistenceConfigurations(connectionString)
    .AddCors()
    .AddJwtAuthentication(builder.Configuration)
    .AddFallbackPolicy()
    .AddClassesConfigurations(builder.Configuration)
    .AddServicesInjection();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors("allowLocalInDevelopment");

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();
app.MapFallbackToFile("index.html");

await app.UpdateDatabase();

app.Run();