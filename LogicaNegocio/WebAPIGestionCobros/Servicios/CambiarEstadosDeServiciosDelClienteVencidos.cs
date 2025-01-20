using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        

        public CambiarEstadosDeServiciosDelClienteVencidos(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //DoWork(null);
            // Configurar el temporizador para que se ejecute todos los días a las 00:01
            var now = DateTime.Now;
            var nextRun = new DateTime(now.Year, now.Month, now.Day, 00, 01, 0).AddDays(1);
            var initialDelay = nextRun - now;

            _timer = new Timer(DoWork, null, initialDelay, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                ServiciosDelClienteController controladorServicios = scope.ServiceProvider.GetRequiredService<ServiciosDelClienteController>();
                try
                {
                    controladorServicios.MarcarServiciosComoVencidos();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al cambiar estados de servicios del cliente a vencidos: {ex.Message}", ex);
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