using DivisionControl.Api;
using DivisionControl.Api.Applications.Commands.Models;
using DivisionControl.Api.Applications.Dtos;
using DivisionControl.IntegrationTests.Configurations;
using DivisionControl.IntegrationTests.Models;
using DivisionControl.UnitTests.Domain.Fixtures;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace DivisionControl.IntegrationTests
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class DividaScenarioTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly IntegrationTestsFixture<Program> _testsFixture;
        private readonly DividaTestFixture _dividaTestFixture;

        private readonly Guid DIVIDA_ID = Guid.Parse("720B7FE8-0661-4331-B214-77F2DEBF5356");

        public DividaScenarioTests(IntegrationTestsFixture<Program> testsFixture)
        {
            _testsFixture = testsFixture;
            _dividaTestFixture = new DividaTestFixture();
        }

        #region Registrar
        [Fact(DisplayName = "Registro de Divida com sucesso")]
        [Trait("Categoria", "Integração - Divida")]
        public async Task Divida_Registrar_DeveExecutarComSucesso()
        {

            // Arrange
            var faker = _dividaTestFixture.GerarDividaValido();
            var parcelas = _dividaTestFixture.ObterParcela(faker.Id);

            var divida = _dividaTestFixture.GerarDividaCompletaValido(faker, parcelas);

            // Act
            using var response = await _testsFixture.Client.PostAsync("/api/Divida/registrar", _testsFixture.MontarContent(divida));

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Registro de Divida com Erro passando Cpf do Devedor Inválido")]
        [Trait("Categoria", "Integração - Divida")]
        public async Task Divida_Registrar_DeveExecutarComErroCpfInvalido()
        {
            // Arrange
            var faker = _dividaTestFixture.GerarDividaComCpfDevedorInvalido();
            var parcelas = _dividaTestFixture.ObterParcela(faker.Id);

            var divida = _dividaTestFixture.GerarDividaCompletaValido(faker, parcelas);

            // Act
            using var response = await _testsFixture.Client.PostAsync("/api/Divida/registrar", _testsFixture.MontarContent(divida));
            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("O CPF informado não é valido.", error.errors);
        }

        [Fact(DisplayName = "Registro de Divida com Campos Inválidos")]
        [Trait("Categoria", "Integração - Divida")]
        public async Task Divida_Registrar_DeveExecutarComErroInvalido()
        {
            // Arrange
            var faker = _dividaTestFixture.GerarDividaCompletaInvalido();

            // Act
            using var response = await _testsFixture.Client.PostAsync("/api/Divida/registrar", _testsFixture.MontarContent(faker));
            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("O número do titulo deve ser informado.", error.errors);
            Assert.Contains("O nome do devedor deve ser informado.", error.errors);
            Assert.Contains("A dívida deve ter pelo menos uma parcela.", error.errors);
            Assert.Contains("O CPF do devedor deve ser informado.", error.errors);
            Assert.Contains("A multa da divida deve ser informado.", error.errors);
            Assert.Contains("O juros da divida deve ser informado.", error.errors);
        }

        #endregion

        #region Atualizar

        [Fact(DisplayName = "Atualizar Divida com sucesso")]
        [Trait("Categoria", "Integração - Divida")]
        public async Task Divida_Atualizar_DeveExecutarComSucesso()
        {
            // Arrange
            var faker = _dividaTestFixture.GerarDividaValido();
            var parcelas = _dividaTestFixture.ObterParcela(faker.Id);

            var divida = _dividaTestFixture.GerarDividaCompletaValidoParaAtualizar(faker, parcelas);

            // Act
            using var response = await _testsFixture.Client.PutAsync("/api/Divida/atualizar", _testsFixture.MontarContent(divida));

            // Assert
            response.EnsureSuccessStatusCode();

        }

        [Fact(DisplayName = "Atualizar Divida com Erro Id Inválido")]
        [Trait("Categoria", "Integração - Divida")]
        public async Task Divida_Atualizar_DeveExecutarComErroIdInvalido()
        {
            // Arrange
            var faker = _dividaTestFixture.GerarDividaValido();
            var parcelas = _dividaTestFixture.ObterParcela(faker.Id);

            var divida = _dividaTestFixture.GerarDividaCompletaInvalidoParaAtualizar(faker, parcelas);

            // Act
            using var response = await _testsFixture.Client.PutAsync("/api/Divida/atualizar", _testsFixture.MontarContent(divida));

            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("O Id informado é inexistente.", error.errors);

        }

        [Fact(DisplayName = "Atualizar de Divida com Erro passando Cpf do Devedor Inválido")]
        [Trait("Categoria", "Integração - Divida")]
        public async Task Divida_Atualizar_DeveExecutarComErroCpfInvalido()
        {
            // Arrange
            var faker = _dividaTestFixture.GerarDividaComCpfDevedorInvalido();
            var parcelas = _dividaTestFixture.ObterParcela(faker.Id);

            var divida = _dividaTestFixture.GerarDividaCompletaValido(faker, parcelas);
         

            // Act
            using var response = await _testsFixture.Client.PutAsync("/api/Divida/atualizar", _testsFixture.MontarContent(divida));
            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("O CPF informado não é valido.", error.errors);
        }

        [Fact(DisplayName = "Atualizar Divida com Campos Inválidos")]
        [Trait("Categoria", "Integração - Divida")]
        public async Task Divida_Atualizar_DeveExecutarComErroInvalido()
        {
            // Arrange
            var faker = _dividaTestFixture.GerarDividaCompletaInvalido();

            // Act
            using var response = await _testsFixture.Client.PutAsync("/api/Divida/atualizar", _testsFixture.MontarContent(faker));
            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("O Id é obrigatório.", error.errors);
            Assert.Contains("O número do titulo deve ser informado.", error.errors);
            Assert.Contains("O nome do devedor deve ser informado.", error.errors);
            Assert.Contains("A dívida deve ter pelo menos uma parcela.", error.errors);
            Assert.Contains("O CPF do devedor deve ser informado.", error.errors);
            Assert.Contains("A multa da divida deve ser informado.", error.errors);
            Assert.Contains("O juros da divida deve ser informado.", error.errors);
        }

        #endregion

        #region Remover

        [Fact(DisplayName = "Remover Divida com sucesso")]
        [Trait("Categoria", "Integração - Divida")]
        public async Task Divida_Remover_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.Parse("6DED18F6-44FA-475F-A11E-DCA7C3B8D7FA");

            // Act
            using var response = await _testsFixture.Client.DeleteAsync($"/api/Divida/remover/{id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Remover Divida com Erro Id Inválido")]
        [Trait("Categoria", "Integração - Divida")]
        public async Task Divida_Remover_DeveExecutarComErroIdObrigatorio()
        {
            // Arrange
            var id = Guid.Parse("00000000-0000-0000-0000-000000000000");

            // Act
            using var response = await _testsFixture.Client.DeleteAsync($"/api/Divida/remover/{id}");
            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("O Id é obrigatório.", error.errors);
            
        }

        [Fact(DisplayName = "Remover Divida com Erro Id Inexistente")]
        [Trait("Categoria", "Integração - Divida")]
        public async Task Divida_Remover_DeveExecutarComErroIdInexistente()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            using var response = await _testsFixture.Client.DeleteAsync($"/api/Divida/remover/{id}");
            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("O Id informado é inexistente.", error.errors);

        }
        #endregion
    }
}
