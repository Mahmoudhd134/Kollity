global using Kollity.Common.ErrorHandling;
using Kollity.Services.API.Extensions;
using Kollity.Services.API.Hubs;
using Kollity.Services.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServicesApiConfigurations();

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
app.MapServicesHubs();
app.MapHealthChecks("healthy");
app.MapFallbackToFile("index.html");

// await app.UpdateDatabase();
app.Run();