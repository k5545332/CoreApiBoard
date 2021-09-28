using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreApiBoard.Interfaces.IRepositorys;
using CoreApiBoard.Interfaces.IServices;
using CoreApiBoard.JWT;
using CoreApiBoard.PostgreSQLModels;
using CoreApiBoard.Repositorys;
using CoreApiBoard.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CoreApiBoard
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
            services.AddCors(options =>
            {
                // CorsPolicy 是自訂的 Policy 名稱
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.WithOrigins("https://littlewhalereactboard.herokuapp.com/")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            //mssql更新資料表
            //Scaffold-DbContext "Server=LAPTOP-8OLRP162;Database=Board;Trusted_Connection=True;User ID=sa;Password=1qaz@WSX" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
            //services.AddDbContext<BoardContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MssqlConnectionString")));


            //PostgreSQL更新資料表
            //Scaffold-DbContext "Host=localhost;Database=Board;Username=postgres;Password=1qaz@WSX" Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir PostgreSQLModels -Force
            //services.AddDbContext<BoardContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreConnectionString")));


            services.AddDbContext<BoardContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreOnlineConnectionString")));



            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration.GetValue<string>("JwtSettings:Issuer"),
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSettings:SignKey")))
                };
            });



            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IThemeRepository, ThemeRepository>();
            services.AddScoped<IThemeService, ThemeService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccessLevelRepository, AccessLevelRepository>();
            
            services.AddScoped<ILoginService, LoginService>();
            
            services.AddHttpContextAccessor();
            services.AddScoped<JwtHelper>();

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

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
