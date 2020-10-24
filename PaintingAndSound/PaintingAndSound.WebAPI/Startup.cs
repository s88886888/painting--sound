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
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "画声博客Api这是个小项目！请不要DD攻击！",//标题
                    Version = "v1", //版本
                     Description = $"Linson 画声博客API v1",    //描述
                    Contact = new OpenApiContact { Name = "Linson", Email = "", Url = new Uri("https://gitee.com/S88888888") },
                    License = new OpenApiLicense { Name = "Linson许可证", Url = new Uri("https://gitee.com/S88888888/painting--sound") }
                });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                //var xmlPath = Path.Combine(basePath, "PaintingAndSound.WebAPI.xml");
                //c.IncludeXmlComments(xmlPath);
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
