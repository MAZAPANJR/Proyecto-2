using MongoDB.Bson;

public class Registro{
    public ObjectId Id{get; set;}
 public string Correo {get; set;}
 public string Contraseña {get; set;}
public string Nombre {get; set;}
}

public class Inicio{
    public ObjectId Id{get; set;}
    public string Correo {get; set;}
    public string Contraseña {get; set;}
    public string OlvidasteTuContraseña {get; set;}
}

public class Recuperacion{
   public ObjectId Id{get; set;}
   public string Correo {get; set;}
}