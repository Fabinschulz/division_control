using DivisionControl.Api.Applications.AutoMapper;
using DivisionControl.Api.Applications.Commands.Handlers;
using DivisionControl.Api.Applications.Commands.Models;
using DivisionControl.Api.Applications.Queries;
using DivisionControl.Core.Communication.Mediator;
using DivisionControl.Domain.Interfaces;
using DivisionControl.Infra.Data.Context;
using DivisionControl.Infra.Data.Repository;
using FluentValidation.Results;
using MediatR;

namespace DivisionControl.Api.Configurations
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
