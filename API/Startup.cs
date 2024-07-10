using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ActivityTrackerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("API")));
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IRunningActivityService, RunningActivityService>();
            services.AddSwaggerGen();
            services.AddControllers();

            // Configure Serilog
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).WriteTo.Console().CreateLogger();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}