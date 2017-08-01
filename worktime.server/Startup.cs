using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace worktime.server
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
            
            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                //builder.AddUserSecrets<Startup>();
            }
            
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            
            //services.AddAuthentication(options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);
            services.AddAuthentication(options => options.SignInScheme = JwtBearerDefaults.AuthenticationScheme);

            services.AddCors();

            services.AddMvc(options =>
            {
                //options.SslPort = 44300;
                options.Filters.Add(new RequireHttpsAttribute ());
            });

            services.AddTransient<Business.User.IUserBL, Business.User.UserBL>();
            services.AddTransient<Data.DataStore.IUserDataStore, Data.DataStore.FileUserDataStore>();
            services.AddTransient<Data.Repository.IUserRepository, Data.Repository.UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            
            var options = new RewriteOptions()
            .AddRedirectToHttps();

            app.UseRewriter(options);

            app.UseStaticFiles();
                        
            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                Authority = JwtSettings.GoogleAuthority, 
                Audience = JwtSettings.GoogleClientId, 
                RequireHttpsMetadata = false,
                AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme,

                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidIssuer = JwtSettings.GoogleValidIssuer
                },
                Events = new JwtBearerEvents(){
                    OnTokenValidated = (context) => {
                        var userbl = app.ApplicationServices.GetService<Business.User.IUserBL>();
                        userbl.UpdateOrCreateUser(context.Ticket.Principal);
                        return Task.FromResult(0);
                    },
                    OnAuthenticationFailed = (context) => {
                        Console.WriteLine(context.Exception);
                        return Task.FromResult(0);
                    }
                }
            });

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            if(env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
