using LogicaAccesoDatos.BaseDatos;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SistemaDeNotificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIGestionCobros.Controllers;
using WebAPIGestionCobros.Servicios;



namespace WebAPIGestionCobros
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:5173")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            
            //para bloquear loops de referencias
            services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //repositorios
            services.AddScoped<IRepositorioClientes, RepositorioClientes>();
            services.AddScoped<IRepositorioServicios, RepositorioServicios>();
            services.AddScoped<IRepositorioServiciosDelCliente, RepositorioServiciosDelCliente>();
            services.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();
            services.AddScoped<IRepositorioDocumentos, RepositorioDocumentos>();
            services.AddScoped<IRepositorioPaises, RepositorioPaises>();
            services.AddScoped<IRepositorioCategorias, RepositorioCategorias>();
            services.AddScoped<IRepositorioMonedas, RepositorioMonedas>();
            services.AddScoped<IRepositorioFrecuencias, RespositorioFrecuencias>();
            services.AddScoped<IRepositorioCobros, RepositorioCobros>();
            services.AddScoped<IRepositorioMediosDePago, RepositorioMediosDePago>();
            services.AddScoped<IRepositorioNotificaciones, RepositorioNotificaciones>();
            services.AddScoped<IRepositorioSuscriptores, RepositorioSuscriptores>();
            services.AddScoped<IRepositorioCotizacionDolar, RepositorioCotizacionDolar>();





            // Registrar NotificacionesController para EnvioAutomatizado
            services.AddScoped<NotificacionesController>();

            // Registrar ServiciosDelClienteController para CambiarEstadosDeServiciosDelClienteVencidos
            services.AddScoped<ServiciosDelClienteController>();

            // Registrar el servicio ActualizarCotizacionDolar
            services.AddSingleton<ActualizarCotizacionDolar>();

            // Registrar el servicio hospedado
            services.AddHostedService<NotificarVencimientosAutomatizados>();
            services.AddHostedService<CambiarEstadosDeServiciosDelClienteVencidos>();
            services.AddHostedService(provider => provider.GetRequiredService<ActualizarCotizacionDolar>());


            // Registra ObservadorService
            services.AddScoped<ObservadorService>();

            // Para uso del sistema de envio de correos
            services.AddScoped<EnviarCorreo>();
            



            //conexión a bd
            string stringConexion = Configuration.GetConnectionString("Miconexion");
            services.AddDbContextPool<CobrosContext>(options => options.UseSqlServer(stringConexion));



            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Activar la política de CORS
            app.UseCors("AllowSpecificOrigin");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
