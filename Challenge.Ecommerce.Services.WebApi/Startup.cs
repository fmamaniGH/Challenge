using AutoMapper;
using Challenge.Ecommerce.Application.Interface;
using Challenge.Ecommerce.Application.Main;
using Challenge.Ecommerce.Comun;
using Challenge.Ecommerce.Domain.Core;
using Challenge.Ecommerce.Domain.Interface;
using Challenge.Ecommerce.Infrastructure.Data;
using Challenge.Ecommerce.Infrastructure.Interface;
using Challenge.Ecommerce.Infrastructure.Repository;
using Challenge.Ecommerce.Infrastructure.Repository.UnitOfWork;
using Challenge.Ecommerce.Mapper;
using Challenge.Ecommerce.Services.WebApi.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Challenge.Ecommerce.Transversal.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Challenge.Ecommerce.Services.WebApi
{
    public class Startup
    {
        readonly string myPolicy = "policyApiChallenge";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("Connection");
            services.AddDbContext<ApplicationContext>(c => c.UseSqlServer(connectionString));
            services.AddCors(options => options.AddPolicy(myPolicy, builder => builder.WithOrigins(Configuration["Config:OriginCors"])
                                                                                        .AllowAnyHeader()
                                                                                        .AllowAnyMethod()));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            // Auto Mapper Configurations, a partir version 3.0. Sin automapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UsuarioMappingsProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            var appSettingsSection = Configuration.GetSection("Config");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();

            services.AddScoped<IBusquedaApplication, BusquedaApplication>();
            services.AddScoped<IPaisApplication, PaisApplication>();
            services.AddScoped<IUsuarioApplication, UsuarioApplication>();
            services.AddScoped<IUsuarioDomain, UsuarioDomain>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddSingleton<IUnitOfWork>(option => new UnitOfWork(new ApplicationContext(connectionString)));

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var Issuer = appSettings.Issuer;
            var Audience = appSettings.Audience;

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userId = int.Parse(context.Principal.Identity.Name);
                        return Task.CompletedTask;
                    },

                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = Issuer,
                    ValidateAudience = true,
                    ValidAudience = Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //No se usa version a partir 3.0
            //services.AddControllers().AddNewtonsoftJson(x =>
            //x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            AddSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Enable Middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Challenge");
            });

            app.UseCors(myPolicy);
            app.UseAuthentication();

            //Habilita los endpoints
            app.UseRouting();

            //Habilita las capacidades de autorizacion en web api
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }



        private void AddSwagger(IServiceCollection services)
        {

            #region AppSettings
            string authorizationUrl = Configuration.GetSection("AuthSettings").GetSection("AuthorizationUrl").Value;
            string tokenUrl = Configuration.GetSection("AuthSettings").GetSection("TokenUrl").Value;
            string scope = Configuration.GetSection("AuthSettings").GetSection("Scope").Value;
            #endregion AppSettings

            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Api Challenge Versi?n 1.0",
                    Version = groupName,
                    Description = "Endpoints Rest Api Challenge",
                    TermsOfService =  new Uri("http://challenge.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "Anonimo ",
                        Email = "fmamani@gmail.com",
                        Url = new Uri("https://localhost.com/contact"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url= new Uri("https://localhost.com/license")
                    }

                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor ingrese JWT con Bearer en el campo",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                //Seguridad global del tipo OAuth 2 y usar un token del tipo portador
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
                });
            });
        }
    }
}
