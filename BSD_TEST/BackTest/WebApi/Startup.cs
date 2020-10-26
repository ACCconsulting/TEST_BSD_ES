using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Vehicle;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Persistence;

namespace WebApi
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

            //Agregar los Cors para que cualquier pueda hacer peticiones
            services.AddCors(o => o.AddPolicy("corsApp", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddControllers();
            //Injection of MeditatR
            services.AddMediatR(typeof(GetAllVehicle.GetAllVehiclesHandler).Assembly);
            //Injection dbcontext, y le pasamos la cadena de conexion
            services.AddDbContext<AplicationContext>(opt=>
            {
                var conect = Configuration.GetSection("ConnectionString").GetValue<string>("DefaultConection");
                opt.UseSqlServer(conect);
            });


            services.AddAutoMapper(typeof(Aplication.Vehicle.New.VehicleNewHandler));

            services.AddSwaggerGen(conf =>
            {
                conf.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TEST REACT CON NET CORE",
                    Version = "v1"

                });
                //conf.CustomSchemaIds(e => e.FullName);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("corsApp");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Descomentar cuando se publique en produccion
           // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "Vehicle/swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/Vehicle/swagger/v1/swagger.json", "TestService");

            });
        }
    }
}
