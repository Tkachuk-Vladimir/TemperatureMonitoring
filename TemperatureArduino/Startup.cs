using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TemperatureArduino.Domain;
using TemperatureArduino.Domain.Repositories.Abstract;
using TemperatureArduino.Domain.Repositories.EntityFramework;
using TemperatureArduino.Service;


namespace TemperatureArduino
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

            //подключаем конфиг из appsetting.json
            Configuration.Bind("Project", new Config());

            //подключаем нужный функционал приложени€ в качестве сервисов
            services.AddTransient<ITemperatureRepository, EFTemperatureRepository>();

            //подключаем контекст
            services.AddDbContext<AppDbContext>(x => x.UseSqlite(Config.ConnectionString));

            services.AddScoped<AppDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //в процессе разработки что бы видеть информацию об ошибке
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
            //подключаем поддержку статических файлов в придложении(css,js и тд)
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
