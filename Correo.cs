using System.Net.Mail;
public class Correo{
    public string Destinatario {get; set;}= "";
    public string Asunto {get; set;}="";
    public string Mensaje {get; set;}="";
    
    public void Enviar(){
     System.Net.Mail.MailMessage correo=new System.Net.Mail.MailMessage();
     correo.From= new System.Net.Mail.MailAddress("arichard040607@gmail.com", null, System.Text.Encoding.UTF8);
     correo. To.Add (Destinatario);
     correo.Subject= this.Asunto;
     correo.Body= this.Mensaje;
     correo.IsBodyHtml= true;
     correo.Priority= MailPriority.Normal;
     SmtpClient smtp = new SmtpClient();
     smtp.UseDefaultCredentials= false;
     smtp.Host = "smtp.gmail.com";
     smtp.Port= 25;
     smtp.Credentials= new System.Net.NetworkCredential ("arichard040607@gmail.com", "qmmv qmix gkej slav");
     smtp.EnableSsl =true;
     smtp.Send(correo);

    }
}
