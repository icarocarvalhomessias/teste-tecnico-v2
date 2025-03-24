using FluentValidation.Results;
using MediatR;

namespace Thunders.TechTest.ApiService.Common;

/// <summary>
/// Class base para comandos.
/// </summary>
public abstract class Command : IRequest<ValidationResult>
{
    public DateTime Timestamp { get; private set; }

    public ValidationResult ValidationResult { get; set; }


    protected Command()
    {
        Timestamp = DateTime.Now;
    }

    public virtual bool IsValid()
    {
        throw new NotImplementedException();
    }

}
