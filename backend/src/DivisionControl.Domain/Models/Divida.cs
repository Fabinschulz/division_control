using DivisionControl.Core.DomainObjects;
using System.Runtime.InteropServices;

namespace DivisionControl.Domain.Models
{
    public class Divida : Entity, IAggregateRoot
    {
        public string NumeroDoTitulo { get; private set; }
        public string NomeDoDevedor { get; private set; }
        public string CpfDoDevedor { get; private set; }
        public decimal Juros { get; private set; }
        public decimal Multa { get; private set; }
        public ICollection<Parcela> Parcelas { get; private set; }

        public decimal ValorAtualizado
        {
            get => CalcularValorAtualizado();
        }

        public decimal ValorOriginal
        {
            get => CalcularValorOriginal();
        }

        public decimal QuantidadeDeParcelas
        {
            get => Parcelas.Count;
        }

        public int DiasEmAtraso
        {
            get => CalcularTotalDiasEmAtraso();
        }

        private Divida(string numeroDoTitulo,
                       string nomeDoDevedor,
                       string cpfDoDevedor,
                       decimal juros,
                       decimal multa)
        {
            NumeroDoTitulo = numeroDoTitulo;
            NomeDoDevedor = nomeDoDevedor;
            CpfDoDevedor = cpfDoDevedor;
            Juros = juros;
            Multa = multa;
        }

        protected Divida() { }

        public void AdicionarParcelas(ICollection<Parcela>? parcelas) => Parcelas = parcelas;

        public decimal CalcularValorAtualizado()
        {
            decimal valorOriginal = CalcularValorOriginal();

            if (DiasEmAtraso > 0)
            {
                decimal juros = Juros / 30 * DiasEmAtraso;
                decimal multa = AplicarMulta(valorOriginal);
                valorOriginal += juros + multa;
            }

            return Math.Round(valorOriginal, 2);
        }

        protected decimal CalcularValorOriginal()
        {
            decimal valorParcelasSomado = 0;

            foreach (Parcela parcela in Parcelas)
            {
                valorParcelasSomado += parcela.ValorDaParcela;
            }

            return valorParcelasSomado;
        }

        public decimal AplicarMulta(decimal valorOriginal)
        {
            var calcularMulta = valorOriginal * (Multa / 100);

            return Math.Round(calcularMulta, 2);
        }

        protected int CalcularTotalDiasEmAtraso()
        {
            int diasAtraso = 0;

            foreach (var parcela in Parcelas)
            {
                diasAtraso += parcela.CalcularDiasAtraso();
            }

            return diasAtraso;
        }

        public void AtualizarDivida(string numeroDoTitulo,
                                    string nomeDoDevedor,
                                    string cpfDoDevedor,
                                    decimal juros,
                                    decimal multa)
        {
            NumeroDoTitulo = numeroDoTitulo;
            NomeDoDevedor = nomeDoDevedor;
            CpfDoDevedor = cpfDoDevedor;
            Juros = juros;
            Multa = multa;
        }

        public static class Factory
        {
            public static Divida CriarDivida(string numeroDoTitulo,
                                             string nomeDoDevedor,
                                             string cpfDoDevedor,
                                             decimal juros,
                                             decimal multa) => new(numeroDoTitulo,
                                                                   nomeDoDevedor,
                                                                   cpfDoDevedor,
                                                                   juros,
                                                                   multa);
        }

    }
}
