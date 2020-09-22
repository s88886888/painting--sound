using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PaintingAndSound.DataAccess.Services;
using PaintingAndSound.Entities;
using PaintingAndSound.ORM;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllersWithViews();
            // 配置使用 Sql Server 的 EF Context
            services.AddDbContext<HSDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HSDemo")));
            services.AddScoped<IEntityRepository<Radio>, EntityRepository<Radio>>();
            services.Configure<CookiePolicyOptions>(options =>
            {
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


                #region 读取xml信息
                var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "PaintingAndSound.WebAPI.xml");//这个就是刚刚配置的xml文件名

                var xmlModelPath = Path.Combine(basePath, "PaintingAndSound.WebAPI.xml");//这个就是Model层的xml文件名

                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改
                c.IncludeXmlComments(xmlModelPath);
                #endregion

                //开启加权小锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //在Heder中添加Token 传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}(注意两者之间有一个空格)",
                    Name = "Authorization",//jwt默认的参数名称,
                    In = ParameterLocation.Header,//jwt默认存放Autorization信息的位置（header中）
                    Type = SecuritySchemeType.ApiKey
                });



            });





            services.AddControllers();
            #endregion
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());//单独角色
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));//或的关系
                options.AddPolicy("SystemAndAdmin", policy => policy.RequireRole("Admin").RequireRole("System"));//且的关系
            });


            //#region 参数
            ////读取配置文件
            //var audienceConfig = Configuration.GetSection("Audience");
            //var symmetricKeyAsBase64 = audienceConfig["Secret"];
            //var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            //var signingKey = new SymmetricSecurityKey(keyByteArray);
            //#endregion

            //var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            services.AddAuthentication(x =>
            {
                //看这个单词熟悉么？没错，就是上边错误里的那个。
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })// 也可以直接写字符串，AddAuthentication("Bearer")
                  .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        //IssuerSigningKey = signingKey,//参数配置在下边
                        ValidateIssuer = true,
                        //ValidIssuer = audienceConfig["Issuer"],//发行人
                        ValidateAudience = true,
                        //ValidAudience = audienceConfig["Audience"],//订阅人
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,//这个是缓冲过期时间，也就是说，即使我们配置了过期时间，这里也要考虑进去，过期时间+缓冲，默认好像是7分钟，你可以直接设置为0
                        RequireExpirationTime = true,
                    };

                });






        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //先开启认证
            app.UseAuthentication();
            //授权中间件
            app.UseAuthorization();
            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"XUnit.Core V1");
                c.RoutePrefix = "";
                //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件
                // c.RoutePrefix = "swagger"; // 如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "swagger"; 则访问路径为 根域名/swagger/index.html
            });



            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
