public static class CalificacionesRequestHandlers{
    public static IResult MostrarCalificaciones(long NumControl,
        int parcial, bool soloAsignaturas, bool soloAcreditadas){


        List<CalificacionMateria> list = new List<CalificacionMateria>();
        CalificacionMateria m1 = new CalificacionMateria();
        m1.Calificacion=7;
        m1.Materia= "Programación Orientada a Objetos";
        m1.Parcial =1;
        m1.NumControl = 22328051051234;

        CalificacionMateria m2= new CalificacionMateria();
        m2.Calificacion=7;
        m2.Materia= "Programación Orientada a Eventos";
        m2.Parcial =1;
        m2.NumControl = 22328051051234;
        
        CalificacionMateria m3= new CalificacionMateria();
        m3.Calificacion=7.2;
        m3.Materia= "Geometria analitica";
        m3.Parcial =1;
        m3.NumControl = 22328051051234;

        CalificacionMateria m4= new CalificacionMateria();
        m4.Calificacion=6;
        m4.Materia= "Biologia";
        m4.Parcial =1;
        m4.NumControl = 22328051051234;

        CalificacionMateria m5= new CalificacionMateria();
        m5.Calificacion=8;
        m5.Materia= "Ingles";
        m5.Parcial =1;
        m5.NumControl = 22328051051234;
        
        CalificacionMateria m6= new CalificacionMateria();
        m6.Calificacion=10;
        m6.Materia= "Tutoria";
        m6.Parcial =1;
        m6.NumControl = 22328051051234;

        CalificacionMateria m7= new CalificacionMateria();
        m7.Calificacion=8;
        m7.Materia= "Etica";
        m7.Parcial =1;
        m7.NumControl = 22328051051234;

        list.Add(m1);
        list.Add(m2);
        list.Add(m3);
        list.Add(m4);
        list.Add(m5);
        list.Add(m6);
        list.Add(m7);

       return Results.Ok(list);
    }


    public static IResult MostrarCalificacionesAlumno(long NumControl){
        if(NumControl ==22328051051234){
         List<CalificacionMateria>list = new List<CalificacionMateria>();

         CalificacionMateria m1= new CalificacionMateria();
         m1.Calificacion=10;
         m1.Materia= "Programacion Orientada a Objetos";
         m1.Parcial=1;
         m1.NumControl=22328051051234;

         list.Add(m1);
         return Results.Ok(list);
        }
        else{
            return Results.NotFound($"No existe un alumno con numero de control{NumControl}");
        }
    }
}
