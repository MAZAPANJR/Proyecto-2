using MongoDB.Driver;
public static class CategoriasRequestHandler {
 public static IResult Crear(CategoriaDTO dto)
{
    // Validar que venga el nombre de la categoría
    if (string.IsNullOrWhiteSpace(dto.Nombre))
    {
        return Results.BadRequest("El nombre de la categoría es obligatorio.");
    }

    // Validar que se haya capturado UrlIcono
    if (string.IsNullOrWhiteSpace(dto.UrlIcono))
    {
        return Results.BadRequest("La URL del ícono es obligatoria.");
    }

    // Resto del código para crear la categoría
    var filterBuilder = new FilterDefinitionBuilder<CategoriaDbMap>();
    var filter = filterBuilder.Eq(x => x.Nombre, dto.Nombre);
    BaseDatos bd = new BaseDatos();
    var coleccion = bd.ObtenerColeccion<CategoriaDbMap>("Categorias");
    CategoriaDbMap? registro = coleccion.Find(filter).FirstOrDefault();

    if (registro != null)
    {
        return Results.BadRequest($"La categoría '{dto.Nombre}' ya existe en la base de datos");
    }
    
    registro = new CategoriaDbMap();
    registro.Nombre = dto.Nombre;
    registro.UrlIcono = dto.UrlIcono;

    coleccion!.InsertOne(registro);
    string nuevoId = registro.Id.ToString();
    return Results.Ok(nuevoId);
  }
  public static IResult Listar(){
    var filterBuilder = new FilterDefinitionBuilder<CategoriaDbMap>();
    var filter = filterBuilder.Empty;

    BaseDatos bd = new BaseDatos();
    var coleccion = bd.ObtenerColeccion<CategoriaDbMap>("Categorias");
    List<CategoriaDbMap> mongoDBList = coleccion.Find(filter).ToList();
    var lista = mongoDBList.Select(x => new {
        Id = x.Id.ToString(),
        Nombre = x.Nombre,
        UrlIcono = x.UrlIcono
  }).ToList();
  return Results.Ok(lista); 
}
}
