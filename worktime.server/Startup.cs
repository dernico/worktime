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
            

            //services.AddMvc();
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
            

            var gClientId = "793729558350-58etvsoelqbc8pi5lknlven67esr03vh.apps.googleusercontent.com"; // Configuration["Authentication:Google:ClientId"];
            var gClientSecret = "jjgBcDv28Uqz-VaEueBX4Gwb"; //Configuration["Authentication:Google:ClientSecret"];
            var redirectUrl = "http://localhost:3000/auth/google/callback";

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                Authority = "https://accounts.google.com", 
                Audience = gClientId, 
                RequireHttpsMetadata = false,
                //AutomaticAuthenticate = true,
                AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme,

                //AutomaticChallenge = false,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidIssuer = "accounts.google.com"
                }
            });


            // app.UseCookieAuthentication(new CookieAuthenticationOptions
            // {
            //     AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme,
            //     AutomaticAuthenticate = true,
            //     AutomaticChallenge = true
            // });
            
            var openIdOptions = new OpenIdConnectOptions
            {
                ClientId = gClientId,
                ClientSecret = gClientSecret,
                Authority = "https://accounts.google.com",
                ResponseType = OpenIdConnectResponseType.CodeIdToken,
                //GetClaimsFromUserInfoEndpoint = true,
                SaveTokens = true,
                Events = new OpenIdConnectEvents()
                {
                    OnUserInformationReceived = (context) => {

                        return Task.FromResult(0);
                    },
                    OnTokenResponseReceived = (context) => {

                        return Task.FromResult(0);
                    },
                    OnTokenValidated = (context) => {
                        var nameClaim = context.Ticket.Principal.Claims.Where(c => c.Type == "name").FirstOrDefault();
                        // if(nameClaim != null){
                        //     var accessToken = context.TokenEndpointResponse.AccessToken;
                        // }
                        return Task.FromResult(0);
                    },
                    OnRedirectToIdentityProvider = (context) =>
                    {
                        if (context.Request.Path != "/auth/external")
                        {
                            context.Response.Redirect("/auth/login");
                            context.HandleResponse();
                        }
 
                        return Task.FromResult(0);
                    }
                }
            };
            openIdOptions.Scope.Add("https://www.googleapis.com/auth/plus.login");
            openIdOptions.Scope.Add("openid");

            //app.UseOpenIdConnectAuthentication(openIdOptions);

            /*
                Authentication:Google:ClientSecret = Pe0fLF09PLeW5qoWC2N_LrQq
                Authentication:Google:ClientID = 793729558350-am6jcbjdcb9b1re7n4imbs2a2bhd18s9.apps.googleusercontent.com
             */
            
            // app.UseGoogleAuthentication(new GoogleOptions()
            // {
            //     SignInScheme = "lala",
            //     ClientId = gClientId,
            //     ClientSecret = gClientSecret,
            // });

            
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
