using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Thunders.TechTest.ApiService.Controllers;

/// <summary>
/// Classe base para os controllers
/// </summary>
[ApiController]
public abstract class MainController : Controller
{
    protected ICollection<string> Erros = new List<string>();

    protected ActionResult CustomResponse(object result = null)
    {
        if (OperacaoValida())
        {
            return Ok(result);
        }

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
    }

    protected bool ResponsePossuiErros(ValidationResult resposta)
    {
        foreach (var erro in resposta.Errors)
        {
            AdicionarErroProcessamento(erro.ErrorMessage);
        }
        return OperacaoValida();
    }

    protected bool OperacaoValida()
    {
        return !Erros.Any();
    }

    protected void AdicionarErroProcessamento(string erro)
    {
        Erros.Add(erro);
    }

    protected void LimparErrosProcessamento()
    {
        Erros.Clear();
    }

}