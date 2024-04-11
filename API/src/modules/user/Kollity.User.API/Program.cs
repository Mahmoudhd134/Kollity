using Kollity.User.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddUserApiConfigurations();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("allowLocalInDevelopment");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();