using AutoMapper;
using DivisionControl.Api.Applications.Commands.Models;
using DivisionControl.Api.Applications.Dtos;
using DivisionControl.Api.Applications.Queries;
using DivisionControl.Core.Communication.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace DivisionControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DividaController : ApiController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IDividaQuerie _dividaQuerie;
        private readonly IMapper _mapper;

        public DividaController(IMediatorHandler mediator, IMapper mapper, IDividaQuerie dividaQuerie)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dividaQuerie = dividaQuerie;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarDividaDto viewModel)
        {
            var commando = _mapper.Map<RegistrarDividaCommand>(viewModel);

            return CustomResponse(await _mediator.EnviarComando(commando));
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarDividaDto viewModel)
        {
            var commando = _mapper.Map<AtualizarDividaCommand>(viewModel);

            return CustomResponse(await _mediator.EnviarComando(commando));
        }

        [HttpDelete("remover/{id:guid}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            var commando = _mapper.Map<RemoverDividaCommand>(id);

            return CustomResponse(await _mediator.EnviarComando(commando));
        }

        [HttpGet("obter/{id:guid}")]
        public async Task<IActionResult> Obter(Guid id)
        {
            return CustomResponse(await _dividaQuerie.ObterPorId(id));
        }

        [HttpGet("obter-listagem")]
        public async Task<IActionResult> ObterListagem()
        {
            return CustomResponse(await _dividaQuerie.ObterListagem());
        }
    }
}
