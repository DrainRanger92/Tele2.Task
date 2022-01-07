using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Tele2.Task.Interaction;
using System.IO;

namespace Tele2.Task
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
            string Get(string @string) => Configuration.GetValue<string>(@string);
            
            services.AddHttpClient("PeopleGetter", c =>
            {
                c.BaseAddress = new(Get("Remote"));
            });

            services.AddDbContext<DwellersContext>(builder =>
            {
                builder.UseMySql(Get("mysql-dev"), new MySqlServerVersion(Get("mysql-version")));
            });

            services.AddSingleton<DwellersManager>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tele2.Task", Version = "v1" });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Swagger-Doc.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tele2.Task v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Starts DwellersManager service by requesting People data and putting them into a database
            app.ApplicationServices.GetService<DwellersManager>().Initialize();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
