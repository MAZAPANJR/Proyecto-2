using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.RegularExpressions;

public static class LenguajeRequestHandler{
    public static IResult ListarRegistros(string idCategoria){
    var filterBuilder = new FilterDefinitionBuilder<LenguajeDbMap>();
    var filter = filterBuilder.Eq(x => x.IdCategoria, idCategoria);

    BaseDatos bd = new BaseDatos();
    var coleccion = bd.ObtenerColeccion<LenguajeDbMap>("LEnguaje");
    var lista = coleccion.Find(filter).ToList();

    return Results.Ok(lista.Select(x => new{
        Id = x.Id.ToString(),
        IdCategoria = x.IdCategoria,
        Titulo = x.Titulo,
        Descripcion = x.Descripcion,
        EsVideo = x.Esvideo,
        Url = x.Url
    }).ToList());    
    }

    public static IResult CrearRegistro(LenguajeDTO dto){
    //Validar que el usuario haya capturado todos los registros

    //Validar que el objeto id tenga el format válido
    if(!ObjectId.TryParse(dto.IdCategoria,out ObjectId IdCategorias)){
        return Results.BadRequest($"El Id de la categoria ({dto.IdCategoria}) no es válido");
    }
    BaseDatos bd = new BaseDatos();

 //Validar que exista la categoría
    var filterBuilderCategorias = new FilterDefinitionBuilder<CategoriaDbMap>();
    var filterCategoria = filterBuilderCategorias.Eq(x => x.Id, IdCategorias);
    var coleccionCategoria = bd.ObtenerColeccion<CategoriaDbMap>("Categorias");
    var categoria = coleccionCategoria.Find(filterCategoria).FirstOrDefault();

    if(categoria == null){
      return Results.NotFound($"No existe una categoria con ID = '{dto.IdCategoria}'");
    }

    LenguajeDbMap registro = new LenguajeDbMap();
    registro.Titulo = dto.Titulo;
    registro.Esvideo = dto.Esvideo;
    registro.Descripcion = dto.Descripcion;
    registro.Url = dto.Url;
    registro.IdCategoria = dto.IdCategoria;

    var coleccionLenguaje = bd.ObtenerColeccion<LenguajeDbMap>("LEnguaje");
    coleccionLenguaje!.InsertOne(registro);

    return Results.Ok(registro.Id.ToString());
    }

    public static IResult Eliminar(string id){
        if(!ObjectId.TryParse(id, out ObjectId idLenguaje)){
         return Results.BadRequest($"El Id proporcionado ({id}) no es válido");
        }

        BaseDatos bd = new BaseDatos();
        var filterBuilder = new FilterDefinitionBuilder<LenguajeDbMap>();
        var filter = filterBuilder.Eq(x => x.Id, idLenguaje);
        var coleccion = bd.ObtenerColeccion<LenguajeDbMap>("LEnguaje");
        coleccion!.DeleteOne(filter);

        return Results.NoContent();
    }

    public static IResult Buscar(string texto){
        var queryExpr = new BsonRegularExpression(new Regex(texto, RegexOptions.IgnoreCase));
        var filterBuilder = new FilterDefinitionBuilder<LenguajeDbMap>();
        var filter =  filterBuilder.Regex("Titulo", queryExpr) |
         filterBuilder.Regex("Descripcion", queryExpr);

        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerColeccion<LenguajeDbMap>("LEnguaje");
        var lista = coleccion.Find(filter).ToList();

        return Results.Ok(lista.Select(x => new{
            Id = x.Id.ToString(),
            IdCategoria = x.IdCategoria,
            Titulo = x.Titulo,
            Descripcion = x.Descripcion,
            EsVideo = x.Esvideo,
            Url = x.Url
        }). ToList());
    }             
}

