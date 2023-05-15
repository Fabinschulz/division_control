using ControleDeDividas.Api.Applications.Commands.Models;
using ControleDeDividas.Core.Communication.Messages;
using ControleDeDividas.Domain.Interfaces;
using ControleDeDividas.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ControleDeDividas.Api.Applications.Commands.Handlers
{
    public class DividaCommandHandler : CommandHandler,
        IRequestHandler<RegistrarDividaCommand, ValidationResult>,
        IRequestHandler<AtualizarDividaCommand, ValidationResult>,
        IRequestHandler<RemoverDividaCommand, ValidationResult>
    {
        private readonly IDividaRepository _dividaRepository;

        public DividaCommandHandler(IDividaRepository dividaRepository)
        {
            _dividaRepository = dividaRepository;
        }

        public async Task<ValidationResult> Handle(RegistrarDividaCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var dividaExistente = await ObterDividaPorCpfDevedor(message.CpfDoDevedor);

            if (dividaExistente != null)
            {
                AdicionarErro("O Cpf informado, já foi cadastrado!");
                return ValidationResult;
            }

            var titulo = Divida.Factory.CriarDivida(message.NumeroDoTitulo,
                                                    message.NomeDoDevedor,
                                                    message.CpfDoDevedor,
                                                    message.Juros,
                                                    message.Multa);

            var parcelasCriada = CriarParcelas(message, titulo.Id);

            titulo.AdicionarParcelas(parcelasCriada);
            _dividaRepository.Adicionar(titulo);

            return await PersistirDados(_dividaRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AtualizarDividaCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var dividaExistente = await ObterDividaPorId(message.Id);

            if (dividaExistente is null)
            {
                AdicionarErro("O Id informado é inexistente.");
                return ValidationResult;
            }

            dividaExistente.AtualizarDivida(message.NumeroDoTitulo,
                                            message.NomeDoDevedor,
                                            message.CpfDoDevedor,
                                            message.Juros,
                                            message.Multa);

            _dividaRepository.Atualizar(dividaExistente);

            var parcelasCriada = CriarParcelas(message, dividaExistente.Id);

            _dividaRepository.RemoverParcelas(dividaExistente.Parcelas);

            _dividaRepository.AdicionarParcelas(parcelasCriada);

            return await PersistirDados(_dividaRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoverDividaCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var dividaExistente = await ObterDividaPorId(message.Id);

            if (dividaExistente is null)
            {
                AdicionarErro("O Id informado é inexistente.");
                return ValidationResult;
            }

            _dividaRepository.RemoverParcelas(dividaExistente.Parcelas);
            _dividaRepository.Remover(dividaExistente);

            return await PersistirDados(_dividaRepository.UnitOfWork);
        }

        private static List<Parcela> CriarParcelas(DividaCommand message, Guid tituloId)
        {
            List<Parcela> parcelas = new();

            if (message.Parcelas.Count > 0)
            {
                foreach (var parcela in message.Parcelas)
                {
                    parcelas.Add(
                        Parcela.Factory.CriarParcela(tituloId,
                                                     parcela.NumeroDaParcela,
                                                     parcela.DataDeVencimento,
                                                     parcela.ValorDaParcela));
                }
            }

            return parcelas;
        }

        private async Task<Divida> ObterDividaPorCpfDevedor(string cpf)
        {
            return await _dividaRepository.ObterPorCpf(cpf);
        }

        private async Task<Divida> ObterDividaPorId(Guid id)
        {
            return await _dividaRepository.ObterPorId(id);
        }
    }
}
