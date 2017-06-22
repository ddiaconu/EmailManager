using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EmailManager.Data;
using EmailManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using EmailManager.Services;
using EmailManager.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using EmailManager.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EmailManager
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;
        private readonly DbContextFactory _ctxFactory;
        private readonly string _connString;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _config = builder.Build();
            _ctxFactory = new DbContextFactory();
            _connString = AppSettings.Get<string>("DbContextConnection");
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.

            services.AddSingleton(_config);

            if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing"))
            {
                services.AddScoped<IMailService, DebugMailService>();
            }
            else
            {
                // Implement a real Mail Service
            }

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(_connString));

            services.AddScoped(p => new DatabaseContext(p.GetService<DbContextOptions<DatabaseContext>>()));

            services.AddUnitOfWork<DatabaseContext>();

            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequireDigit = false;
                config.Password.RequiredLength = 8;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = true;
                config.Password.RequireLowercase = false;
                config.Cookies.ApplicationCookie.LoginPath = "/auth/login";
                config.Cookies.ApplicationCookie.LogoutPath = "/auth/logout";
                config.Cookies.ApplicationCookie.AccessDeniedPath = "/Account/AccessDenied";
                config.Cookies.ApplicationCookie.AutomaticAuthenticate = false;
                config.Cookies.ApplicationCookie.AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                          ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        await Task.Yield();
                    }
                };
            })
            .AddEntityFrameworkStores<DatabaseContext>();

            services.AddScoped<IRepo<EnEvent>, Repo<EnEvent>>();

            services.AddTransient<SeedData>();

            //services.AddMvc();

            services.AddMvc(config =>
            {
                if (_env.IsProduction())
                {
                    config.Filters.Add(new RequireHttpsAttribute());
                }
            })
            .AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, SeedData seeder)
        {
            //Mapper.Initialize(config =>
            //{
            //    config.CreateMap<EnEventsViewModel, EnEvent>().ReverseMap();
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                app.UseExceptionHandler("/App/Error");
                loggerFactory.AddDebug(LogLevel.Error);
            }

            loggerFactory.AddConsole(_config.GetSection("Logging"));

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=App}/{action=Index}/{id?}");
            });

            seeder.EnsureSeed().Wait();
        }
    }
}
