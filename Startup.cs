using Assignment10.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10
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
            services.AddControllersWithViews();
            //add sqlite db
            services.AddDbContext<BowlingLeagueContext>(options =>
               options.UseSqlite(Configuration["ConnectionStrings:BowlingLeagueDbConnection"]));
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //make url pretty for the ~aesthetic~
                //displays selected team name and current page num in url
                endpoints.MapControllerRoute("bowlerteampagenum",
                    "BowlerTeam/{bowlerteamid}/{bowlerteam}/{pagenum}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });
                //displays selected team name in url
                endpoints.MapControllerRoute("bowlerteamid",
                    "BowlerTeam/{bowlerteamid}/{bowlerteam}",
                    new { Controller = "Home", action = "Index", pageNum=1}
                    );
                //displays current page num in url
                endpoints.MapControllerRoute("pagenum",
                    "{pagenum}",
                    new {Controller = "Home", action = "Index"});
                //default
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
