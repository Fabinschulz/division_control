using ControleDeDividas.Api.Applications.Commands.Handlers;
using ControleDeDividas.Api.Applications.Commands.Models;
using ControleDeDividas.Domain.Interfaces;
using ControleDeDividas.Domain.Models;
using ControleDeDividas.UnitTests.Domain.Fixtures;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace ControleDeDividas.UnitTests.Application.Dividas.Handler
{
    [Collection(nameof(DividaCollection))]
    public class DividaCommandHandlerTest
    {
        private readonly DividaTestFixture _dividaTestFixture;
        private readonly AutoMocker _mocker;
        private readonly DividaCommandHandler _dividaCommandHandler;

        public DividaCommandHandlerTest(DividaTestFixture dividaTestFixture)
        {
            _mocker = new AutoMocker();
            _dividaCommandHandler = _mocker.CreateInstance<DividaCommandHandler>();
            _dividaTestFixture = dividaTestFixture;
        }

        #region Registrar
        [Fact(DisplayName = "Divida deve ser Criado com sucesso")]
        [Trait("Categoria", "Application - Command Handler - Divida")]
        public async Task RegistrarDivida_NovaDivida_Sucesso()
        {
            //Arrange
            var divida = _dividaTestFixture.GerarDividaValido();
            var parcelas = _dividaTestFixture.ObterListaParcela(divida.Id);

            var dividaCommand = new RegistrarDividaCommand(divida.NumeroDoTitulo,
                                                           divida.NomeDoDevedor,
                                                           divida.CpfDoDevedor,
                                                           divida.Juros,
                                                           divida.Multa,
                                                           parcelas);

            _mocker.GetMock<IDividaRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _dividaCommandHandler.Handle(dividaCommand, CancellationToken.None);

            // Assert
            Assert.True(dividaCommand.EhValido());
            _mocker.GetMock<IDividaRepository>().Verify(r => r.Adicionar(It.IsAny<Divida>()), Times.Once);
            _mocker.GetMock<IDividaRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);

        }

        [Fact(DisplayName = "Deve Validar Cpf Do Devedor Existente")]
        [Trait("Categoria", "Application - Command Handler - Divida")]
        public async Task RegistrarDivida_ValidarCpfDevedorDivida_Erro()
        {
            //Arrange
            var divida = _dividaTestFixture.GerarDividaValido();
            var parcelas = _dividaTestFixture.ObterListaParcela(divida.Id);

            var dividaCommand = new RegistrarDividaCommand(divida.NumeroDoTitulo,
                                                           divida.NomeDoDevedor,
                                                           divida.CpfDoDevedor,
                                                           divida.Juros,
                                                           divida.Multa,
                                                           parcelas);

            _mocker.GetMock<IDividaRepository>().Setup(r => r.ObterPorCpf(dividaCommand.CpfDoDevedor)).Returns(Task.FromResult(divida));

            //Act
            var result = await _dividaCommandHandler.Handle(dividaCommand, CancellationToken.None);

            // Assert
            Assert.Contains("O Cpf informado, já foi cadastrado!", result.ToString());
        }

        #endregion

        #region Remover
        [Fact(DisplayName = "Deve Remover uma Divida")]
        [Trait("Categoria", "Application - Command Handler - Divida")]
        public async Task RemoverDivida_Sucesso()
        {
            //Arrange
            var divida = _dividaTestFixture.GerarDividaValido();

            var dividaCommand = new RemoverDividaCommand(divida.Id);

            _mocker.GetMock<IDividaRepository>().Setup(r => r.ObterPorId(dividaCommand.Id)).Returns(Task.FromResult(divida));
            _mocker.GetMock<IDividaRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _dividaCommandHandler.Handle(dividaCommand, CancellationToken.None);

            // Assert
            _mocker.GetMock<IDividaRepository>().Verify(r => r.Remover(It.IsAny<Divida>()), Times.Once);
            _mocker.GetMock<IDividaRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Deve Validar uma Divida Antes de Remover")]
        [Trait("Categoria", "Application - Command Handler - Divida")]
        public async Task RemoverDivida_ValidarDividaExistente_Erro()
        {
            //Arrange
            var divida = _dividaTestFixture.GerarDividaValido();

            var dividaCommand = new RemoverDividaCommand(divida.Id);

            //Act
            var result = await _dividaCommandHandler.Handle(dividaCommand, CancellationToken.None);

            // Assert
            Assert.Contains("O Id informado é inexistente.", result.ToString());
        }
        #endregion

        #region Atualizar

        [Fact(DisplayName = "Divida deve ser Atualizada com sucesso")]
        [Trait("Categoria", "Application - Command Handler - Divida")]
        public async Task AtualizarDivida_AtualizarADivida_Sucesso()
        {
            //Arrange
            var divida = _dividaTestFixture.GerarDividaValido();
            var parcelas = _dividaTestFixture.ObterListaParcela(divida.Id);

            var dividaCommand = new AtualizarDividaCommand(divida.Id,
                                                           divida.NumeroDoTitulo,
                                                           divida.NomeDoDevedor,
                                                           divida.CpfDoDevedor,
                                                           divida.Juros,
                                                           divida.Multa,
                                                           parcelas);

            _mocker.GetMock<IDividaRepository>().Setup(r => r.ObterPorId(dividaCommand.Id)).Returns(Task.FromResult(divida));
            _mocker.GetMock<IDividaRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _dividaCommandHandler.Handle(dividaCommand, CancellationToken.None);

            // Assert
            Assert.True(dividaCommand.EhValido());
            _mocker.GetMock<IDividaRepository>().Verify(r => r.Atualizar(It.IsAny<Divida>()), Times.Once);
            _mocker.GetMock<IDividaRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Deve Validar Divida Inexistente")]
        [Trait("Categoria", "Application - Command Handler - Divida")]
        public async Task AtualizarDivida_ValidarDividaInexistente_Erro()
        {
            //Arrange
            var divida = _dividaTestFixture.GerarDividaValido();
            var parcelas = _dividaTestFixture.ObterListaParcela(divida.Id);

            var dividaCommand = new AtualizarDividaCommand(divida.Id,
                                                           divida.NumeroDoTitulo,
                                                           divida.NomeDoDevedor,
                                                           divida.CpfDoDevedor,
                                                           divida.Juros,
                                                           divida.Multa,
                                                           parcelas);

            _mocker.GetMock<IDividaRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _dividaCommandHandler.Handle(dividaCommand, CancellationToken.None);

            // Assert
            Assert.Contains("O Id informado é inexistente", result.ToString());
        }
        #endregion

    }
}
