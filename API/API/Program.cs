using API.Extensions;
using API.Helpers;
using Application;
using Infrastructure;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenWithJwtAuth();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var connectionString = builder.Configuration["ConnectionStrings:Default"];
builder.Services
    .AddApplicationConfiguration(builder.Configuration)
    .AddPersistenceConfigurations(connectionString)
    .AddInfrastructureServices()
    .AddCors()
    .AddJwtAuthentication(builder.Configuration)
    // .AddFallbackPolicy()
    .AddClassesConfigurations(builder.Configuration)
    .AddServicesInjection()
    .AddModelBindingErrorsMap();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    await app.UpdateDatabase();
}

app.UseHttpsRedirection();

app.UseCors("allowLocalInDevelopment");

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();