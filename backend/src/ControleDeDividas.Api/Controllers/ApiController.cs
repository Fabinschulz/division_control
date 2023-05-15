using ControleDeDividas.Core.Communication.Mediator;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace ControleDeDividas.Api.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidarOperacao())
            {
                return Ok(new
                {
                    status = (int)HttpStatusCode.OK,
                    success = true,
                    errors = Erros.ToArray(),
                    model = result
                }); ;
            }

            return BadRequest(new
            {
                status = (int)HttpStatusCode.BadRequest,
                success = false,
                errors = Erros.ToArray()
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AdicionarMensagemErro(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AdicionarMensagemErro(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool ValidarOperacao()
        {
            return !Erros.Any();
        }

        protected void AdicionarMensagemErro(string error)
        {
            Erros.Add(error);
        }

        protected void LimparErros()
        {
            Erros.Clear();
        }
    }
}
