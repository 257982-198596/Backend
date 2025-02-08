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

        //
        // ENVIAR RENOVACION DE SERVICIOS
        //
        public async Task EnviarRecordatorio(ServicioDelCliente elServicioDelCliente, Cliente elCliente)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("SendGrid API key is not set in the environment variables.");
            }

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("cobros257982198596@gmail.com", "Sistema De Cobros");

            var subject = $"RECORDATORIO DE VENCIMIENTO: {elServicioDelCliente.Descripcion}";
            var to = new EmailAddress($"{elCliente.UsuarioLogin.Email}", $"{elCliente.Nombre}");
            // Calcular días restantes para el vencimiento
            int diasVencimiento = (elServicioDelCliente.FechaVencimiento - DateTime.Today).Days;
            var plainTextContent = $@"
            Estimado Cliente {elCliente.Nombre}:

            Su servicio {elServicioDelCliente.Descripcion} con fecha de vencimiento {elServicioDelCliente.FechaVencimiento:dd/MM/yyyy} le restan {diasVencimiento} días para vencerse.
            
            El costo de renovación es de {elServicioDelCliente.Precio} {elServicioDelCliente.MonedaDelServicio.Nombre}.
            Evite costos adicionales y renueve su servicio a tiempo.

            Datos Bancarios:
            Banco: Banco de la República Oriental del Uruguay   
            Cuenta: 123456789
            Sucursal: 2

            Por cualquier duda o consulta no dude en contactarnos.
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

        // 
        // ENVIO DE CONTRASEÑA TEMPORAL
        // 
        public async Task EnviarContrasenaTemporal(Cliente elCliente, string contrasenaTemporal)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("SendGrid API key is not set in the environment variables.");
            }

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("cobros257982198596@gmail.com", "Sistema De Cobros");

            var subject = "Contraseña Temporal";
            var to = new EmailAddress($"{elCliente.UsuarioLogin.Email}", $"{elCliente.Nombre}");
            var plainTextContent = $@"
            Estimado {elCliente.Nombre}:

            Se ha generado una nueva contraseña temporal para su cuenta. Por favor, utilice la siguiente contraseña para iniciar sesión y cambie su contraseña lo antes posible.

            Contraseña Temporal: {contrasenaTemporal}

            Muchas gracias!

            Sistema de Cobros
            ";
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al enviar correo");
            }
        }
    }

}