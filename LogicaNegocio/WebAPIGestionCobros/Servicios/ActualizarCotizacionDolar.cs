using LogicaNegocio.Dominio;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;

using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class ActualizarCotizacionDolar : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IServiceProvider _serviceProvider;
    private readonly HttpClient _httpClient;
    private readonly ILogger<ActualizarCotizacionDolar> logAzure;

    public ActualizarCotizacionDolar(IServiceProvider serviceProvider, ILogger<ActualizarCotizacionDolar> logger)
    {
        _serviceProvider = serviceProvider;
        _httpClient = new HttpClient();
        logAzure = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        logAzure.LogInformation("Servicio ActualizarCotizacionDolar iniciado.");

        var now = DateTime.Now;
        var nextRun = new DateTime(now.Year, now.Month, now.Day, 18, 0, 0);
        if (now > nextRun)
        {
            nextRun = nextRun.AddDays(1);
        }
        var initialDelay = nextRun - now;

        _timer = new Timer(ObtenerCotizacionCallback, null, initialDelay, TimeSpan.FromDays(1));
        return Task.CompletedTask;
    }

    private async void ObtenerCotizacionCallback(object state)
    {
        logAzure.LogInformation("Ejecutando ObtenerCotizacionCallback.");
        var cotizacion = await ObtenerCotizacion(state);
        if (cotizacion != null)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var repoCotizaciones = scope.ServiceProvider.GetRequiredService<IRepositorioCotizacionDolar>();
                    repoCotizaciones.Add(cotizacion);
                    logAzure.LogInformation("Cotización del dólar actualizada correctamente.");
                }
                catch (Exception ex)
                {
                    logAzure.LogError(ex, "Error al actualizar la cotización del dólar en la base de datos.");
                }

            }
        }


    }

    public async Task<CotizacionDolar> ObtenerCotizacion(object state)
    {
        try
        {
            var response = await _httpClient.GetStringAsync("https://api.exchangerate-api.com/v4/latest/USD");
            var exchangeRates = JsonConvert.DeserializeObject<ExchangeRatesResponse>(response);

            if (exchangeRates != null && exchangeRates.Rates != null && exchangeRates.Rates.TryGetValue("UYU", out var rate))
            {
                return new CotizacionDolar
                {
                    Fecha = DateTime.Now,
                    Valor = rate
                };
            }
        }
        catch (Exception ex)
        {
            logAzure.LogError(ex, "Error al obtener la cotización del dólar.");
            throw new Exception($"Error al obtener la cotización del dólar: {ex.Message}", ex);
        }

        return null;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _httpClient?.Dispose();
    }
}

public class ExchangeRatesResponse
{
    public Dictionary<string, decimal> Rates { get; set; }
}
