using MongoDB.Driver;

public static class UsuariosRequestHandler {
    public static IResult Registrar (Registro datos){
        if (string.IsNullOrWhiteSpace(datos.Nombre)){
            return Results.BadRequest("Ingresa el nombre");
        }
        if (string.IsNullOrWhiteSpace(datos.Correo)){
            return Results.BadRequest("El correo es requerido");
        }
        if (string.IsNullOrWhiteSpace(datos.Contraseña)){
            return Results.BadRequest("La contraseña es requerida");
        }
        BaseDatos bd= new BaseDatos();
        var coleccion =bd.ObtenerColeccion<Registro>("Usuario");
        if (coleccion==null){
            throw new Exception ("No existe la coleccion Usuario");
        }
         
        FilterDefinitionBuilder<Registro> filterBuilder= new FilterDefinitionBuilder<Registro>();
        var filter = filterBuilder.Eq(x=> x.Correo, datos.Correo);
    
        Registro? usuarioExistente = coleccion.Find(filter).FirstOrDefault();
        if(usuarioExistente !=null){
            return Results.BadRequest ($"Ya existe un usuario con el correo {datos.Correo}");
        }
         coleccion.InsertOne(datos);

            return Results.Ok();
    }

    public static IResult Ingresar (Registro datos){
        
        
        if (string.IsNullOrWhiteSpace(datos.Correo)){
            return Results.BadRequest("El correo es requerido");
        }
        if (string.IsNullOrWhiteSpace(datos.Contraseña)){
            return Results.BadRequest("La contraseña es requerida");
        }
        BaseDatos bd= new BaseDatos();
        var coleccion =bd.ObtenerColeccion<Registro>("Usuario");
        if (coleccion==null){
            throw new Exception ("No existe la coleccion Usuario");
        }
         
        FilterDefinitionBuilder<Registro> filterBuilder= new FilterDefinitionBuilder<Registro>();
        var filter = filterBuilder.Eq(x=> x.Correo, datos.Correo);
    
        Registro? usuarioExistente = coleccion.Find(filter).FirstOrDefault();
        if(usuarioExistente ==null){
            return Results.BadRequest ($"No existe un usuario con el correo {datos.Correo}");
        }

        if(usuarioExistente.Contraseña != datos.Contraseña) {
            return Results.BadRequest ($"Contraseña incorrecta");
        }

            return Results.Ok("Usuario correcto " + usuarioExistente.Id.ToString());
    }

     public static IResult Recuperar (RecuperarContraseña datos){
        
        if (string.IsNullOrWhiteSpace(datos.Correo)){
            return Results.BadRequest("El correo es requerido");
        }
        
    BaseDatos bd=new BaseDatos();
    var coleccion=bd.ObtenerColeccion<Registro>("Usuario");
    if (coleccion==null){
        throw new Exception("No existe la coleccion usuario");
    }
    FilterDefinitionBuilder<Registro> filterBuilder=new FilterDefinitionBuilder<Registro>();
    var filter=filterBuilder.Eq(x=>x.Correo, datos.Correo);
    
    Registro? usuarioExistente=coleccion.Find(filter).FirstOrDefault();
    if (usuarioExistente == null){
        return Results.BadRequest($"No existe un usuario con el correo {datos.Correo}");
    }else if (usuarioExistente.Correo==datos.Correo){
        Correo c = new Correo();
        c.Destinatario = usuarioExistente.Correo;
        c.Asunto = "Recuperar contraseña SENA";
        c.Mensaje = $"Tu contraseña es - {usuarioExistente.Contraseña} -";
        c.Enviar();
    }
    

    return Results.Ok();
    }
}



