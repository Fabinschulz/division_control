using DivisionControl.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DivisionControl.Infra.Data.Mappings
{
    public class DividaMapping : IEntityTypeConfiguration<Divida>
    {
        public void Configure(EntityTypeBuilder<Divida> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Ignore(d => d.ValorAtualizado);
            builder.Ignore(d => d.ValorOriginal);
            builder.Ignore(d => d.QuantidadeDeParcelas);

            builder.Property(c => c.NumeroDoTitulo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.NomeDoDevedor)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.CpfDoDevedor)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.Property(p => p.Juros)
                .HasColumnType("decimal(5,2)");

            builder.Property(p => p.Multa)
                .HasColumnType("decimal(5,2)");

            // 1 : N => Divida : Parcelas
            builder.HasMany(c => c.Parcelas)
                .WithOne(c => c.Divida)
                .HasForeignKey(c => c.DividaId );

            builder.ToTable("Dividas");
        }
    }
}
