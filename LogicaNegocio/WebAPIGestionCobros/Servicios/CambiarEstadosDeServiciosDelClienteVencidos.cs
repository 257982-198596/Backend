using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebAPIGestionCobros.Controllers;

namespace WebAPIGestionCobros.Servicios
{
    public class CambiarEstadosDeServiciosDelClienteVencidos : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceProvider ServiceProvider;

        private readonly ILogger<CambiarEstadosDeServiciosDelClienteVencidos> logAzure;

        public CambiarEstadosDeServiciosDelClienteVencidos(IServiceProvider serviceProvider, ILogger<CambiarEstadosDeServiciosDelClienteVencidos> logger)
        {
            ServiceProvider = serviceProvider;
            logAzure = logger;

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            TimeZoneInfo uruguayZone = TimeZoneInfo.FindSystemTimeZoneById("America/Montevideo");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, uruguayZone);
            logAzure.LogInformation("Servicio CambiarEstadosDeServiciosDelClienteVencidos iniciado.");

            var now = DateTime.Now;
            var nextRun = new DateTime(now.Year, now.Month, now.Day, 20, 50, 0);

            if (now > nextRun)
            {
                nextRun = nextRun.AddDays(1);
            }

            var initialDelay = nextRun - now;

            _timer = new Timer(CambiarEstado, null, initialDelay, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        private async void CambiarEstado(object state)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                ServiciosDelClienteController controladorServicios = scope.ServiceProvider.GetRequiredService<ServiciosDelClienteController>();
                try
                {
                    logAzure.LogInformation("Ejecutando MarcarServiciosComoVencidos.");
                    controladorServicios.MarcarServiciosComoVencidos();
                    logAzure.LogInformation("MarcarServiciosComoVencidos ejecutado correctamente.");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al cambiar estados de servicios del cliente a vencidos: {ex.Message}", ex);
                    logAzure.LogError(ex, "Error al cambiar estados de servicios del cliente a vencidos.");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}