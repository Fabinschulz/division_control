using ControleDeDividas.Core.Communication.Mediator;
using ControleDeDividas.Core.Communication.Messages;
using ControleDeDividas.Core.Data;
using ControleDeDividas.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeDividas.Infra.Data.Context
{
    public class SystemContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public SystemContext(DbContextOptions<SystemContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Divida> Dividas { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder
             .Model
             .GetEntityTypes()
             .SelectMany(e => e.GetProperties()
                 .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SystemContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;
            if (success) await _mediatorHandler.PublicarEventos(this);

            return success;
        }
    }
}
