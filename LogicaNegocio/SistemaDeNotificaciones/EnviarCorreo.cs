using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Mail.Model;
using System.Net.Mail;


namespace SistemaDeNotificaciones
{
    public class EnviarCorreo
    {

        

        
        public async Task EnviarEmail()
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("SendGrid API key is not set in the environment variables.");
            }

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("cobros257982198596@gmail.com", "Sistema De Cobros");
            var subject = "Primer EMAIL";
            var to = new EmailAddress("info@zion.com.uy", "ZionWeb");
            var plainTextContent = "Primera prueba mails";
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            
        }

        //
        // ENVIAR RENOVACION DE SERVICIOS
        //

        public async Task EnviarRenovacionServicio(CobroRecibido elCobro, Cliente elCliente)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("SendGrid API key is not set in the environment variables.");
            }

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("cobros257982198596@gmail.com", "Sistema De Cobros");

            var subject = $"RENOVACIÓN DE SERVICIO: {elCobro.ServicioDelCliente.Descripcion}"; 
            var to = new EmailAddress($"{elCliente.UsuarioLogin.Email}", $"{elCliente.Nombre}");
            var plainTextContent = $@"
            Estimado Cliente {elCliente.Nombre}:

            Su servicio {elCobro.ServicioDelCliente.Descripcion} con fecha de vencimiento {elCobro.ServicioDelCliente.FechaVencimiento:dd/MM/yyyy} ha sido renovado satisfactoriamente.

            Muchas gracias!

            Sistema de Cobros
            ";
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al enviar corrreo");
            }
        }


        

    }

}