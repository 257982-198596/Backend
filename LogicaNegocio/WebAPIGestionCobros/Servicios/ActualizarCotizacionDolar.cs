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

public class ActualizarCotizacionDolar : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IServiceProvider _serviceProvider;
    private readonly HttpClient _httpClient;
    

    public ActualizarCotizacionDolar(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _httpClient = new HttpClient();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        ObtenerCotizacionCallback(null);
        // Configurar el temporizador para que se ejecute todos los días a las 10:30
        var initialDelay = TimeSpan.FromMinutes(12);

        _timer = new Timer(ObtenerCotizacionCallback, null, initialDelay, TimeSpan.FromDays(1));
        return Task.CompletedTask;
    }

    private async void ObtenerCotizacionCallback(object state)
    {
        var cotizacion = await ObtenerCotizacion(state);
        if (cotizacion != null)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repoCotizaciones = scope.ServiceProvider.GetRequiredService<IRepositorioCotizacionDolar>();
                repoCotizaciones.Add(cotizacion);
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
