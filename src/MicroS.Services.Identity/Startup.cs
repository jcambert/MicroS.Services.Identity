using Autofac;
using Consul;
using MicroS.Services.Identity.Domain;
using MicroS_Common;
using MicroS_Common.Authentication;
using MicroS_Common.Consul;
using MicroS_Common.Dispatchers;
using MicroS_Common.Jeager;
using MicroS_Common.Mongo;
using MicroS_Common.Mvc;
using MicroS_Common.RabbitMq;
using MicroS_Common.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace MicroS.Services.Identity
{
    public class Startup
    {
        private static readonly string[] Headers = new[] { "X-Operation", "X-Resource", "X-Total-Count" };

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            //services.AddSwaggerDocs();
            services.AddConsul();
            services.AddJwt();
            services.AddJaeger();
            //services.AddOpenTracing();
            services.AddRedis();
            services.AddInitializers(typeof(IMongoDbInitializer));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                        cors
                            //.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .WithExposedHeaders(Headers));
            });
            //services.AddHostedService<LifetimeEventsHostedService>();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                   .AsImplementedInterfaces();
            
            builder.AddMongo();
            builder.AddMongoRepository<RefreshToken>("RefreshTokens");
            builder.AddMongoRepository<User>("Users");
            builder.AddRabbitMq();
            builder.AddDispatchers();
            builder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime, IStartupInitializer initializer
            , IConsulClient consulClient,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseAllForwardedHeaders();
            //app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseAccessTokenValidator();
            app.UseServiceId();
            app.UseMvc();
            app.UseRabbitMq();
            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(consulServiceId);
               
            });
            applicationLifetime.ApplicationStarted.Register(() =>
            {
                logger.LogInformation("Micro Service Identity is started");
            });
            applicationLifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Micro Service Identity is being stopping");
            });


        }
    }
}
