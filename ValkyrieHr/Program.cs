using ValkyrieHr.Installer;

var builder = WebApplication.CreateBuilder(args).RegisterServices();

var app = builder.Build().SetupMiddleware();

app.Run();
