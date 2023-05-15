using ControleDeDividas.Api.Applications.AutoMapper;
using ControleDeDividas.Api.Applications.Commands.Handlers;
using ControleDeDividas.Api.Applications.Commands.Models;
using ControleDeDividas.Api.Applications.Queries;
using ControleDeDividas.Core.Communication.Mediator;
using ControleDeDividas.Domain.Interfaces;
using ControleDeDividas.Infra.Data.Context;
using ControleDeDividas.Infra.Data.Repository;
using FluentValidation.Results;
using MediatR;

namespace ControleDeDividas.Api.Configurations
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //auto mapper
            services.AddAutoMapper(typeof(MappingHelperProfile));

            //Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Repository
            services.AddScoped<SystemContext>();
            services.AddScoped<IDividaRepository, DividaRepository>();

            // Application - Commands
            services.AddScoped<IRequestHandler<RegistrarDividaCommand, ValidationResult>, DividaCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarDividaCommand, ValidationResult>, DividaCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverDividaCommand, ValidationResult>, DividaCommandHandler>();

            // Application - Queries
            services.AddScoped<IDividaQuerie, DividaQuerie>();
        }
    }
}
