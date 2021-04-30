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
                options.SwaggerDoc("UniversityAPISpec",
                                    new Microsoft.OpenApi.Models.OpenApiInfo()
                                    {
                                        Title = "University API",
                                        Version = "1",
                                        Description = "PLC University API",
                                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                                        {
                                            Email = "ht10082001@gmail.com",
                                            Name = "phuocleoceo",
                                            Url = new Uri("https://facebook.com/phuocleoceo")
                                        },
                                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                                        {
                                            Name = "phuocleoceo",
                                            Url = new Uri("https://facebook.com/phuocleoceo")
                                        }
                                    });
                //Lay ten Assembly (Project) hien tai, chinh la ten cua file .xml
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //Combine path cua project voi ten file xml
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(cmlCommentsFullPath);
            });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
