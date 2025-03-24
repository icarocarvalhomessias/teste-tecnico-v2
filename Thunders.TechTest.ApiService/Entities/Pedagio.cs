using Thunders.TechTest.ApiService.Common;
using Thunders.TechTest.ApiService.Entities.Validators;

namespace Thunders.TechTest.ApiService.Entities
{
    public class Pedagio : Entity
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }

        public Guid CidadeId { get; set; }
        public Cidade Cidade { get; set; }

        protected Pedagio() { }

        public Pedagio(string nome, Guid cidadeId, decimal valor)
        {
            Nome = nome;
            CidadeId = cidadeId;
            Valor = valor;

            Validate();
        }

        public ValidationResultDetail Validate()
        {
            var validator = new PedagioValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
