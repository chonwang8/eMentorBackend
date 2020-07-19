using Data.Entities;
using Data.UnitOfWork;
using Data.UnitOfWork.Interfaces;
using Domain.Helper.DataObjects;
using Domain.Services;
using Domain.Services.Interfaces;
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

namespace eMentor
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
            #region Get Config

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSetting>(appSettingsSection);

            #endregion Get Config

            #region Dependency
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IChannelService, ChannelService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IEnrollService, EnrollService>();
            services.AddTransient<IMajorService, MajorService>();
            services.AddTransient<IMenteeService, MenteeService>();
            services.AddTransient<IMentorService, MentorService>();
            services.AddTransient<ISharingService, SharingService>();
            services.AddTransient<ISubscriptionService, SubscriptionService>();
            services.AddTransient<ITopicService, TopicService>();
            services.AddTransient<IUserService, UserService>();
            #endregion

            #region DbConnection
            string ConnectionString = Configuration.GetConnectionString("remote-eMentor-DB");
            #endregion

            #region JWT Auth

            var appSettings = appSettingsSection.Get<AppSetting>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/flutter-chat-ba7c2";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidIssuer = "https://securetoken.google.com/flutter-chat-ba7c2",
                        ValidateAudience = true,
                        ValidAudience = "flutter-chat-ba7c2",
                        ValidateLifetime = false
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            #endregion JWT Auth

            #region CORS
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            #endregion

            #region Entity Framework Core
            services.AddDbContext<eMentorContext>(options =>
                options.UseSqlServer(ConnectionString));
            #endregion

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v5", new OpenApiInfo
                {
                    Title = "eMentor API",
                    Version = "v5",
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "devase insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
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

            #endregion Swagger

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

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v5/swagger.json", "SWD391 V5");
                c.RoutePrefix = "";
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
