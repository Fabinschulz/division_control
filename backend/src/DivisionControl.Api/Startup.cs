using DivisionControl.Api.Configurations;
using DivisionControl.Api.Extensions;
using MediatR;

namespace DivisionControl.Api
{
    public interface IStartup
    {
        IConfiguration Configuration { get; }
        void Configure(WebApplication app, IWebHostEnvironment environment);
        void ConfigureServices(IServiceCollection services);
    }

    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration= configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddApiConfiguration(Configuration);

            services.AddMediatR(typeof(Program));

            services.RegisterServices();
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            app.UseApiConfiguration(environment);

            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
