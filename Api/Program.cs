using Api.Configuration;

var builder = WebApplication
    .CreateBuilder(args);

builder
    .ConfigureServices()
    .ConfigureHost()
    .ConfigureStorage();

var app = builder.Build();

app.ConfigureApplication();
app.Run();

public partial class Program
{
}
