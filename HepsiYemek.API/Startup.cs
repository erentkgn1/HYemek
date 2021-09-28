using HepsiYemek.API.Filters;
using HepsiYemek.API.Middlewares;
using HepsiYemek.API.Settings;
using HepsiYemek.Services.Abstract;
using HepsiYemek.Services.Concrete;
using HepsiYemek.Services.RabbitMQ.Consumers;
using HepsiYemek.Services.Settings;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiYemek.API
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

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDBSettings"));
            services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));
            services.AddScoped(typeof(IProductService), typeof(ProductService));
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
            services.AddScoped<RedisCacheAttribute>();
            services.AddSingleton<RedisService>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
                var redis = new RedisService(settings.Host, settings.Port);
                redis.Connect();
                return redis;
            });


            services.AddMassTransit(x =>
            {
                x.AddConsumer<HepsiYemekMessageCommandComsumer>();
               
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(Configuration["RabbitMQUrl"], "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    cfg.ReceiveEndpoint("update-product", e => {
                        e.ConfigureConsumer<HepsiYemekMessageCommandComsumer>(context);

                    });

                });


            });


             services.AddMassTransitHostedService();



            services.AddAutoMapper(typeof(Startup));
         
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HepsiYemek.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
              app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HepsiYemek.API v1"));
            }

           

            app.ConfigureExceptionHandler();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
