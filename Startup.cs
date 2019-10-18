using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstDemo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public delegate void HelloFunctionDelegate(string Message);

namespace FirstDemo
{
    public class Startup
    {
        private IConfiguration _config; // Used for dependency injection

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddMvc().AddXmlSerializerFormatters(); // Adds all required services for MVC
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>(); // Tells the system which implementation of IEmployeeRepository to inject
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");                      // Global exception handler - redirects to ErrorController
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();
            app.UseAuthentication(); //! Must be placed before MVC middleware

            //Conventional Routing
            //? Creates a rout to the home controller's details action method
            app.UseMvc(routes => {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); 
            }); // Middleware for MVC when using routes
        }
    }
}
