using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using PaintingAndSound.DataAccess.Services;
using PaintingAndSound.Entities;
using PaintingAndSound.ORM;
using PaintingAndSound.UserAndRole;
using PaintingAndSound.WebAPI.JWT;
using Swashbuckle.AspNetCore.Filters;

namespace PaintingAndSound.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string ApiName { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AppSettings.Init(Configuration);
            services.AddControllers().AddNewtonsoftJson(setup =>
            {

                setup.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            });


            services.AddControllersWithViews();
            services.AddDbContext<HSDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HsContext")));
            services.AddAutoMapper(Assembly.Load("PaintingAndSound.ViewModel"));





            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "画声博客Api这是个小项目！请不要DD攻击！",//标题
                    Version = "v1", //版本
                    Description = $"Linson 画声博客API v1",    //描述
                    Contact = new OpenApiContact { Name = "Linson", Email = "", Url = new Uri("https://gitee.com/S88888888") },
                    License = new OpenApiLicense { Name = "Linson许可证", Url = new Uri("https://gitee.com/S88888888/painting--sound") }
                });

                //在发布时，XML不可使用

                //var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                //// 这个就是刚刚配置的xml文件名
                //var xmlPath = Path.Combine(basePath, "PaintingAndSound.WebAPI.xml");
                //// 默认的第二个参数是false，这个是controller的注释，记得修改
                //c.IncludeXmlComments(xmlPath, true);

                //var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                //var xmlPath = Path.Combine(basePath, "PaintingAndSound.WebAPI.xml");//这个就是刚刚配置的xml文件名
                //c.IncludeXmlComments(xmlPath, true); // 这个是controller的注释
            });




            services.AddControllers();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEntityRepository<Radio>, EntityRepository<Radio>>();
            services.AddScoped<IEntityRepository<Painting>, EntityRepository<Painting>>();
            services.AddScoped<IEntityRepository<WorksComments>, EntityRepository<WorksComments>>();
            services.AddScoped<IEntityRepository<Works>, EntityRepository<Works>>();
            services.AddScoped<IEntityRepository<PaintionPhotos>, EntityRepository<PaintionPhotos>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "画声API");
            });

            app.UseRouting();

            app.UseAuthorization();

            //CORS 中间件必须配置为在对 UseRouting 和 UseEndpoints的调用之间执行。 配置不正确将导致中间件停止正常运行。
            app.UseCors("any");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
