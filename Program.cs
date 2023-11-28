using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>(options =>
    options.SerializerOptions.PropertyNamingPolicy=null);

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapGet("/", () => "Hello World!");

app.MapGet("/control-escolar/alumnos",AlumnosRequestHandlers.ListarAlumnos);

app.MapPost("/usuarios/registrar",UsuariosRequestHandler.Registrar);
app.MapPost("/usuarios/ingresar",UsuariosRequestHandler.Ingresar);
app.MapPost("/usuarios/recuperar",UsuariosRequestHandler.Recuperar);
app.MapPost("/categorias/crear",CategoriasRequestHandler.Crear);
app.MapGet("/categorias/listar",CategoriasRequestHandler.Listar);
app.MapGet("/lenguaje/{idCategoria}",LenguajeRequestHandler.ListarRegistros);
app.MapPost("/lenguaje",LenguajeRequestHandler.CrearRegistro);
app.MapDelete("/lenguaje/{id}",LenguajeRequestHandler.Eliminar);
app.MapGet("/lenguaje/buscar", LenguajeRequestHandler.Buscar);
app.Run();