using DivisionControl.Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Rewrite;

namespace DivisionControl.Api.Configurations
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDatabaseConfiguration(configuration);
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();           

        }

        public static void UseApiConfiguration(this WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();


            app.MapControllers();

            app.UseCors();

            app.UseDeveloperExceptionPage();          

        }
    }
}
