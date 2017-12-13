using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using pokelist.Model;
using pokelist.Services;

namespace pokelist
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

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration =
                    Configuration.GetConnectionString("Redis");
                options.InstanceName = Configuration.GetSection("ApplicationSettings").GetValue<string>("RedisInstanceName");
            });

            services.AddSingleton<IPokemonService, PokemonApiService>();

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "My Pokemon API" });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Pokemon API");
            });

            app.UseMvc();
        }
    }
}
