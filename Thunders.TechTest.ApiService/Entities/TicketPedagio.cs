using Thunders.TechTest.ApiService.Common;
using Thunders.TechTest.ApiService.Entities.Validators;

namespace Thunders.TechTest.ApiService.Entities;

/// <summary>
/// Representa um ticket de pedágio.
/// </summary>
public class TicketPedagio : Entity, IAggregateRoot
{
    /// <summary>
    /// Obtém o valor do ticket de pedágio.
    /// </summary>
    public decimal Valor { get; private set; }

    /// <summary>
    /// Obtém a data de utilização do ticket de pedágio.
    /// </summary>
    public DateTime DataUtilizacao { get; private set; }

    /// <summary>
    /// Obtém o ID do pedágio associado ao ticket.
    /// </summary>
    public Guid PedagioId { get; private set; }

    /// <summary>
    /// Obtém a praça de pedágio associada ao ticket.
    /// </summary>
    public Pedagio PraçaPedagio { get;  set; }

    /// <summary>
    /// Obtém o tipo de veículo associado ao ticket.
    /// </summary>
    public TipoVeiculo TipoVeiculo { get; private set; }

    /// <summary>
    /// Construtor protegido para uso pelo Entity Framework.
    /// </summary>
    protected TicketPedagio() { }

    /// <summary>
    /// Construtor para criar um novo ticket de pedágio.
    /// </summary>
    /// <param name="pedagioId">ID do pedágio.</param>
    /// <param name="valor">Valor do ticket.</param>
    /// <param name="tipoVeiculo">Tipo de veículo.</param>
    public TicketPedagio(Guid pedagioId, decimal valor, TipoVeiculo tipoVeiculo)
    {
        PedagioId = pedagioId;
        Valor = valor;
        DataUtilizacao = DateTime.UtcNow;
        TipoVeiculo = tipoVeiculo;

        Validate();
    }

    /// <summary>
    /// Verifica se o ticket de pedágio é válido.
    /// </summary>
    /// <returns>Retorna true se o ticket for válido, caso contrário, false.</returns>
    public bool IsValid()
    {
        return Validate().IsValid;
    }

    public void AlteradataUtilizacao(DateTime dataUtilizacao)
    {
        DataUtilizacao = dataUtilizacao;
    }

    /// <summary>
    /// Valida o ticket de pedágio.
    /// </summary>
    /// <returns>Retorna um objeto <see cref="ValidationResultDetail"/> contendo o resultado da validação.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new TicketPedagioValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
