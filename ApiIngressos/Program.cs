using ApiIngressos.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

app.MapUsuariosEndpoints();
app.MapCuponsEndpoints();
app.MapEventosEndpoints();