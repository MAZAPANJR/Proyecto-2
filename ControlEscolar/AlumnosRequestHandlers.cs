using MongoDB.Bson;
using MongoDB.Driver;

public static class AlumnosRequestHandlers{
    public static IResult ListarAlumnos(){
        string connectiongString="mongodb+srv://estefanyguadalupematildeacevedogen22:cbtis105@cluster0.zmu9boy.mongodb.net/?retryWrites=true&w=majority";
        MongoClient client = new MongoClient(connectiongString);
        
        var collection=client.GetDatabase("ControlEscolar").GetCollection<Alumno>("Alumnos");
        FilterDefinitionBuilder<Alumno> filters = new FilterDefinitionBuilder<Alumno>();
        var list = collection.Find(filters.Empty).ToList();
        return Results.Ok(list);
    }
}