using Apresentacao.Entities.DAO;
using Apresentacao.Entities.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Apresentacao
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Injecao de dependencia do DAO e da conexao do banco
            services.AddTransient<ILoginDAO, LoginDAO>();
            services.AddDbContext<DBContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Configura Session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            // Add framework services.


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Roda os Migrations caso o banco de dados não exista
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DBContext>();
                context.Database.Migrate();
            }

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Shared/Error");
            //    app.UseHsts();
            //}

            app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Adiciona o uso de Atributos pra fazer autorização de paginas
            app.UseAuthorization();

            // Adiciona o uso de Session na aplicação
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
