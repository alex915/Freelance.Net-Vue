using Auth;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using VueCliMiddleware;

namespace ProyectoOesia
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

            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "spa/dist";
            //});

            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));

            services.AddDbContext<Data.ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration["ConnectionString"], b => b.MigrationsAssembly("Data")));


            services.AddIdentityCore<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;

            }).AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            services
                .AddAuthorization()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Proyecto Oesia", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT containing userid claim",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                var security =
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                },
                                UnresolvedReference = true
                            },
                            new List<string>()
                        }
                    };
                c.AddSecurityRequirement(security);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Proyecto Oesia v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
           
            if (!env.IsDevelopment())
            {
                //app.UseSpaStaticFiles();
            }
            app.UseStaticFiles();
            app.UseFileServer();
            app.UseDefaultFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
           
            if (env.IsDevelopment())
            {
                //app.UseSpa(spa =>
                //{
                //    spa.Options.SourcePath = "spa";
                //    spa.UseVueCli(

                //        npmScript: "serve",
                //        port: 8080
                //        );
                //    // spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                //});
            }
        }
    }
}
