namespace DivisionControl.Api.Applications.Dtos
{
    public class ParcelasDto
    {
        public string NumeroDaParcela { get; set; }
        public DateTime DataDeVencimento { get; set; }
        public decimal ValorDaParcela { get; set; }
        public int DiasEmAtraso { get; set; }
    }
}
