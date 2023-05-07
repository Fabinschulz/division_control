using DivisionControl.Core.DomainObjects;

namespace DivisionControl.Domain.Models
{
    public class Parcela : Entity
    {
        public Guid DividaId { get; private set; }
        public string NumeroDaParcela { get; private set; }
        public DateTime DataDeVencimento { get; private set; }
        public decimal ValorDaParcela { get; private set; }

        /* EF Relation */
        public virtual Divida Divida { get; private set; }

        public int DiasEmAtraso
        {
            get => CalcularDiasAtraso();
        }

        private Parcela(Guid dividaId, string numeroDaParcela, DateTime dataDeVencimento, decimal valorDaParcela)
        {
            DividaId = dividaId;
            NumeroDaParcela = numeroDaParcela;
            DataDeVencimento = dataDeVencimento;
            ValorDaParcela = valorDaParcela;
        }

        protected Parcela() { }

        public int CalcularDiasAtraso()
        {
            DateTime hoje = DateTime.Today;
            int diasAtraso = 0;


            if (DataDeVencimento < hoje)
            {
                diasAtraso += (hoje - DataDeVencimento).Days;
            }

            return diasAtraso;
        }

        public static class Factory 
        {
            public static Parcela CriarParcela(
                Guid dividaId,
                string numeroDaParcela,
                DateTime dataDeVencimento,
                decimal valorDaParcela) => new Parcela(dividaId, numeroDaParcela, dataDeVencimento, valorDaParcela);
        }

    }
}
