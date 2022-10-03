using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;

namespace Prescribing_System
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
            services.AddMemoryCache();
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //Admin routes
                endpoints.MapAreaControllerRoute(
                    name: "pagin_and_sorting",
                    areaName: "Admin",
                    pattern: "Admin/{controller}/{action}/Page/{pageNumber}/Sort-by-{sortBy}");
                endpoints.MapAreaControllerRoute(
                    name: "paging",
                    areaName: "Admin",
                    pattern: "Admin/{controller}/{action}/Page/{pageNumber}");
                endpoints.MapAreaControllerRoute(
                    name: "admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}/{slug?}");
                endpoints.MapAreaControllerRoute(
                    name: "patient",
                    areaName: "Patient",
                    pattern: "Patient/{controller=Home}/{action=Index}/{id?}/{slug?}");
                endpoints.MapAreaControllerRoute(
                    name: "doctor",
                    areaName: "Doctor",
                    pattern: "Doctor/{controller=Home}/{action=Index}/{id?}/{slug?}");
                //Pharmacist routes
                endpoints.MapAreaControllerRoute(
                    name:"patient_prescription",
                    areaName: "Pharmacist",
                    pattern: "Pharmacist/{controller}/{action}/{idNumber}");
                endpoints.MapAreaControllerRoute(
                    name: "pharmacist",
                    areaName: "Pharmacist",
                    pattern: "Pharmacist/{controller=Home}/{action=Index}/{id?}/{slug?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

            });
        }
    }
}
