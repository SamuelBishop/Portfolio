using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using InternalForcesGUI.Models;
using InternalForcesGUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InternalForcesGUI
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
            services.AddRazorPages();

            // Adds a service that comes and goes (always new). Informs system about service.
            services.AddTransient<JsonFileUserInputService>();

            // Every time you make a new controller make sure to add it into the startup cs
            services.AddControllers(); // Adds all controllers

            // To use blazor components you need to add in blazor as well
            services.AddServerSideBlazor();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers(); // Maps all of the controllers as endpoints
                endpoints.MapBlazorHub();
                // User a Controller instead of doing the following

                // Adding a new endpoint. Implementation of API.
                //endpoints.MapGet("/userInput", (context) =>
                //{
                //    // How to access a service from outside the services folder
                //    // Apparently this is a gross manual way to use an API
                //    var userInput = app.ApplicationServices.GetService<JsonFileUserInputService>().GetuserInput();
                //    var json = JsonSerializer.Serialize<IEnumerable<userInput>>(userInput);
                //    return context.Response.WriteAsync(json);
                //});
            });
        }
    }
}
