using System.ComponentModel.DataAnnotations;

namespace Thunders.TechTest.ApiService.Entities;

public abstract class Entity
{
    [Required]
    public Guid Id { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime? DataUltimaAtualizacao { get; set; }

    public bool Ativo { get; set; } = true;

    protected Entity()
    {
        Id = Guid.NewGuid();
    }
}
