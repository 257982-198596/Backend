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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                    builder => builder.WithOrigins("http://localhost:5174")
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
