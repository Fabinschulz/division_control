using ControleDeDividas.Domain.Models;

namespace ControleDeDividas.Api.Applications.Dtos
{
    public class DividaDto
    {
        public Guid Id { get; set; }
        public string NumeroDoTitulo { get; set; }
        public string NomeDoDevedor { get; set; }
        public string CpfDoDevedor { get; set; }
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        public int QuantidadeDeParcelas { get; set; }
        public decimal ValorOriginal { get; set; }
        public int DiasEmAtraso { get; set; }
        public decimal ValorAtualizado { get; set; }
        public ICollection<ParcelasDto>? Parcelas { get; set; }
    }
}
