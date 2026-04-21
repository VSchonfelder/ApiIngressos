using ApiIngressos.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton(new DbConnectionFactory(connectionString));

var app = builder.Build();

// chama os endpoints separados
app.MapEventosEndpoints();
app.MapUsuariosEndpoints();
app.MapCuponsEndpoints();

app.Run();
