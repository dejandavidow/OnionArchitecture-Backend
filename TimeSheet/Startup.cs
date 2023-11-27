using Contracts;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Repositories;
using Persistence.Services;
using Services;
using Services.Abstractions;
using Services.MappingProfile;
using System.Text;
using TimeSheet.Extensions;
namespace TimeSheet
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
            services.AddAutoMapper(typeof(ServiceProfiles));
            services.Configure<MailSettings>(Configuration.GetSection("EmailConfiguration"));
            services.AddCors();
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "https://localhost:44381/",
                        ValidAudience = "https://localhost:44381/",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                    };
                });
            services.AddDbContext<RepositoryDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<RepositoryDbContext>()
            .AddDefaultTokenProviders();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.TryAddScoped<ITimeSheetService, TimeSheetService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMailService, MailService>();
            services.AddControllers();
            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TimeSheet v1")
                    );
            }
            app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(options => options
            .WithOrigins(new[] { "http://localhost:3000", "http://localhost:4200" })
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            );
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
