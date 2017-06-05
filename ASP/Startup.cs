using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.DataAccess;
using Infrastructure.IoC.Modules;
using Infrastructure.Repositories;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MySQL.Data.EntityFrameworkCore.Extensions;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.Text;

namespace ASP
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddScoped<IUserRepository, DatabaseUserRepository>();
            services.AddMemoryCache();
            services.AddMvc();

            services.AddDbContext<LandlordContext>(options =>
            {
                //Error at Tests executing
                //options.UseMySQL(Configuration.GetConnectionString("LandlordMySQLConnection"));

                options.UseMySQL("server=localhost;userid=root;pwd=;port=3306;database=Landlord;sslmode=none;");
            });
            
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule(new SettingsModule(Configuration));
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<AutoMapperModule>();
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            var AuthSettings = app.ApplicationServices.GetService<AuthenticationSettings>();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = AuthSettings.Issuer,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthSettings.PrivateKey))
                }
            });
            
            app.UseMvc();
            app.AddNLogWeb();
            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());

            env.ConfigureNLog("nlog.config");
        }
    }
}
