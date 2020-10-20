using System;
using System.IO;
using System.Reflection;
using System.Text;
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
            AppSettings.Init(Configuration);
            services.AddControllers().AddNewtonsoftJson(setup=> {

                setup.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            
            });


            services.AddControllersWithViews();
            // ����ʹ�� Sql Server �� EF Context
            services.AddDbContext<HSDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HsContext")));


            services.AddAutoMapper(Assembly.Load("PaintingAndSound.ViewModel"));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaintingAndSound", Version = "v1.2019.10.19" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "���¿�����������ͷ����Ҫ���Jwt��ȨToken��Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme{
                                Reference = new OpenApiReference {
                                            Type = ReferenceType.SecurityScheme,
                                            Id = "Bearer"}
                           },new string[] { }
                        }
                    });
            });

            services
              .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidIssuer = AppSettings.JwtSetting.Issuer,
                      ValidAudience = AppSettings.JwtSetting.Audience,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JwtSetting.SecurityKey)),
                      // Ĭ������ 300s  ��ʱ��ƫ����������Ϊ0
                      ClockSkew = TimeSpan.Zero,
                  };
              });

            services.AddCors(options =>
            {
                options.AddPolicy("any",
                    builder =>
                    {
                        builder.AllowAnyMethod()
                            .AllowAnyOrigin()
                            .AllowAnyHeader();
                    });
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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            //CORS �м����������Ϊ�ڶ� UseRouting �� UseEndpoints�ĵ���֮��ִ�С� ���ò���ȷ�������м��ֹͣ�������С�
            app.UseCors("any");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
