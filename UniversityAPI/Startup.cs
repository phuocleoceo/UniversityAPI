using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Data;
using UniversityAPI.Repository.IRepository;
using UniversityAPI.Repository;
using AutoMapper;
using UniversityAPI.Mapper;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace UniversityAPI
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
            services.AddCors();
            services.AddDbContext<APIContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUniversityRepository, UniversityRepository>();
            services.AddScoped<IPathWayRepository, PathWayRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(UniversityMapping));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("UniversityAPISpec", new OpenApiInfo()
                {
                    Title = "University API",
                    Version = "1",
                    Description = "PLC University API",
                    Contact = new OpenApiContact()
                    {
                        Email = "ht10082001@gmail.com",
                        Name = "phuocleoceo",
                        Url = new Uri("https://facebook.com/phuocleoceo")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "phuocleoceo",
                        Url = new Uri("https://facebook.com/phuocleoceo")
                    }
                });
                //Lay ten Assembly (Project) hien tai, chinh la ten cua file .xml
                string xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //Combine path cua project voi ten file xml
                string cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(cmlCommentsFullPath);

                //Add BearerJWT 
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization
                                  Enter 'Bearer ' and then your token in the text input below   
                                  Example: Bearer 123456789",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Scheme="oauth2",
                            Name="Bearer",
                            In=ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            byte[] key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings").Get<AppSettings>().Secret);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(c =>
            {
                c.RequireHttpsMetadata = false;
                c.SaveToken = true;
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/UniversityAPISpec/swagger.json", "University API");
                options.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
