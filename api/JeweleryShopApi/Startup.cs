using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeweleryShopApi.Database;
using JeweleryShopApi.Helpers;
using JeweleryShopApi.Infrastructure;
using JeweleryShopApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace JeweleryShopApi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JeweleryShopApi", Version = "v1" });
            });
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IUserAuthService, UserAuthService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddControllers(options => options.Filters.Add(new ResponseExceptionFilter()));
            services.AddDbContext<JeweleryShopDbContext>(options => options.UseInMemoryDatabase(databaseName: "JeweleryShop"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JeweleryShopApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<JwtAuthentication>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
