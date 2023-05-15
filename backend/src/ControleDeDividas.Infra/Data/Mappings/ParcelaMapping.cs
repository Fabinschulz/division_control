using ControleDeDividas.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ControleDeDividas.Infra.Data.Mappings
{
    public class ParcelaMapping : IEntityTypeConfiguration<Parcela>
    {
        public void Configure(EntityTypeBuilder<Parcela> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Ignore(d => d.DiasEmAtraso);

            builder.Property(c => c.NumeroDaParcela)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.ValorDaParcela)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.ToTable("Parcelas");
        }
    }
}
