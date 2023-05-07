namespace DivisionControl.Api.Applications.Dtos
{
    public class AtualizarDividaDto
    {
        public Guid Id { get; set; }
        public string NumeroDoTitulo { get; set; }
        public string NomeDoDevedor { get; set; }
        public string CpfDoDevedor { get; set; }
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        public ICollection<ParcelasDto> Parcelas { get; set; }
    }
}
