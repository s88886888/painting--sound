using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PaintingAndSound.DataAccess.Services;
using PaintingAndSound.Entities;
using PaintingAndSound.ORM;

namespace PaintingAndSound.WebAPI
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

            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddTransient(typeof(RadioServiceIDAL<>), typeof(RadioServiceDAL<>));
            services.AddControllersWithViews();
            // 配置使用 Sql Server 的 EF Context
            services.AddDbContext<HSDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HSDemo")));
            services.AddScoped<IEntityRepository<Radio>, EntityRepository<Radio>>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",   //版本 
                    Title = $"XUnit.Core 接口文档-NetCore3.1",  //标题
                    Description = $"XUnit.Core Http API v1",    //描述
                    Contact = new OpenApiContact { Name = "闷骚捞", Email = "132@qq。com", Url = new Uri("http://www.baidu.com") },
                    License = new OpenApiLicense { Name = "闷骚捞许可证", Url = new Uri("http://www.baidu.com") }
                });
            });
            services.AddControllers();
            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
