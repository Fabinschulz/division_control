using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using DivisionControl.Api.Applications.Dtos;
using DivisionControl.Domain.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DivisionControl.UnitTests.Domain.Fixtures
{

    [CollectionDefinition(nameof(DividaCollection))]
    public class DividaCollection : ICollectionFixture<DividaTestFixture> { }
  
  
    public class DividaTestFixture
    {
        public string CPF_INVALIDO = "00000000000";
        private readonly Guid DIVIDA_ID = Guid.Parse("2DFE1C3C-57D5-4B49-AF09-9817AB1C64D6");
        private readonly Guid DIVIDA_ID_INEXISTENTE = Guid.Parse("8F7F89EF-E00F-4102-81B1-B561B2C6B49A");

        private readonly Faker<Divida> Faker;
      
        public DividaTestFixture()
        {
            Faker = new Faker<Divida>("pt_BR");
        }

        public Divida GerarDividaValido()
        {
            var divida = Faker.CustomInstantiator(f =>
                Divida.Factory.CriarDivida(
                   f.Random.AlphaNumeric(10),
                      f.Person.FullName,
                         f.Person.Cpf(),
                            f.Random.Decimal(0.01m, 0.1m),
                                f.Random.Decimal(0.01m, 0.1m)));

            return divida;
        }

        public Divida GerarDividaCalculoValido()
        {
            var divida = Faker.CustomInstantiator(f =>
                Divida.Factory.CriarDivida(
                   f.Random.AlphaNumeric(10),
                      f.Person.FullName,
                         f.Person.Cpf(),
                            0.04168630258447087M,
                                0.05992659174252682M));

            return divida;
        }

        public Divida GerarDividaComCpfDevedorInvalido()
        {
            var divida = Faker.CustomInstantiator(f =>
                Divida.Factory.CriarDivida(
                   f.Random.AlphaNumeric(10),
                      f.Person.FullName,
                         CPF_INVALIDO,
                            f.Random.Decimal(0.01m, 0.1m),
                                f.Random.Decimal(0.01m, 0.1m)));

            return divida;
        }

        public RegistrarDividaDto GerarDividaCompletaInvalido()
        {
            var divida = Divida.Factory.CriarDivida(string.Empty, string.Empty, string.Empty, 0, 0);

            return new RegistrarDividaDto()
            {
                CpfDoDevedor = divida.CpfDoDevedor,
                Juros = divida.Juros,
                Multa = divida.Multa,
                NomeDoDevedor = divida.NomeDoDevedor,
                NumeroDoTitulo = divida.NumeroDoTitulo,
                Parcelas = new List<ParcelasDto>()
            };
        }

        public ICollection<Parcela> ObterListaDeParcelas(Guid dividaId)
        {
            List<Parcela> parcelas = new()
            {
                 Parcela.Factory.CriarParcela(dividaId, "1", DateTime.Parse("27/09/2024"), 100m),
                 Parcela.Factory.CriarParcela(dividaId, "2", DateTime.Parse("27/10/2024"), 100m)
            };

            return parcelas;
        }

        public RegistrarDividaDto GerarDividaCompletaValido(Divida divida, Parcela parcela)
        {
            return new RegistrarDividaDto()
            {
                CpfDoDevedor = divida.CpfDoDevedor,
                Juros = divida.Juros,
                Multa = divida.Multa,
                NomeDoDevedor = divida.NomeDoDevedor,
                NumeroDoTitulo = divida.NumeroDoTitulo,
                Parcelas = new List<ParcelasDto>()
                {
                    new ParcelasDto()
                    {
                        DataDeVencimento = parcela.DataDeVencimento,
                        DiasEmAtraso  = parcela.DiasEmAtraso,
                        NumeroDaParcela = parcela.NumeroDaParcela,
                        ValorDaParcela = parcela.ValorDaParcela
                    }
                }
            };
        }

        public AtualizarDividaDto GerarDividaCompletaValidoParaAtualizar(Divida divida, Parcela parcela)
        {
            return GerarDividaAtualizar(divida, parcela, DIVIDA_ID);
        }

        public AtualizarDividaDto GerarDividaCompletaInvalidoParaAtualizar(Divida divida, Parcela parcela)
        {
            return GerarDividaAtualizar(divida, parcela, DIVIDA_ID_INEXISTENTE);
        }

        public AtualizarDividaDto GerarDividaAtualizar(Divida divida, Parcela parcela, Guid id)
        {
            return new AtualizarDividaDto()
            {
                Id = id,
                CpfDoDevedor = divida.CpfDoDevedor,
                Juros = divida.Juros,
                Multa = divida.Multa,
                NomeDoDevedor = divida.NomeDoDevedor,
                NumeroDoTitulo = divida.NumeroDoTitulo,
                Parcelas = new List<ParcelasDto>()
                {
                    new ParcelasDto()
                    {
                        DataDeVencimento = parcela.DataDeVencimento,
                        DiasEmAtraso  = parcela.DiasEmAtraso,
                        NumeroDaParcela = parcela.NumeroDaParcela,
                        ValorDaParcela = parcela.ValorDaParcela
                    }
                }
            };
        }

        public Parcela ObterParcela(Guid dividaId)
        {
            return Parcela.Factory.CriarParcela(dividaId, "1", DateTime.Parse("27/09/2024"), 100m);
        }

        public List<Parcela> ObterListaParcela(Guid dividaId)
        {
            return new List<Parcela>()
            {
                 Parcela.Factory.CriarParcela(dividaId, "1", DateTime.Parse("27/09/2024"), 100m)
            };

        }

        public List<Parcela> ObterListaParcelaCompleta(Guid dividaId)
        {
            return new List<Parcela>()
            {
                Parcela.Factory.CriarParcela(Guid.NewGuid(), "000", DateTime.Now, 10),
                Parcela.Factory.CriarParcela(Guid.NewGuid(), "000", DateTime.Now, 1000),
                Parcela.Factory.CriarParcela(Guid.NewGuid(), "000", DateTime.Now, 220),
                Parcela.Factory.CriarParcela(Guid.NewGuid(), "000", DateTime.Now, 20),
            };

        }

        public List<Parcela> ObterListaParcelaCalcularCalculos(Guid dividaId)
        {
            return new List<Parcela>()
            {
                Parcela.Factory.CriarParcela(dividaId, "001", DateTime.Today.AddDays(-5), 100m),
                Parcela.Factory.CriarParcela(dividaId, "002", DateTime.Today.AddDays(-10), 200m),
                Parcela.Factory.CriarParcela(dividaId, "003", DateTime.Today.AddDays(-15), 300m)
            };

        }

    }
}
