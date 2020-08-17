using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Avtobus1.BLL.Implementation;
using Avtobus1.BLL.Interfaces;
using Avtobus1.BLL.Services.Mapping;
using Avtobus1.DAL.Context;
using Avtobus1.Models.Entity;
using Avtobus1.Models.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Avtobus1
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration  configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt =>
                opt.UseMySql("server=localhost;UserId=root;Password=pass;database=avtobus1;", 
                    mySqlOptions =>
                    {
                        mySqlOptions.ServerVersion(new Version(5, 7, 17), ServerType.MariaDb)
                            .EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }));

            services.AddScoped<IUrlDisplayer, UrlDisplayer>();
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile<ViewCreateLinkToEntity>();
            }, typeof(Startup));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            app.UseMvc(routes =>
            {
                routes.MapRoute("default",
                "{controller=UrlList}/{action=Index}/{id?}");
            });
        }
    }
}
