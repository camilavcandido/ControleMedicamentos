using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public class ValidadorFornecedor : AbstractValidator<Fornecedor>
    {
        public ValidadorFornecedor()
        {
            RuleFor(x => x.Nome)
             .NotNull().WithMessage("O campo nome é obrigatório")
             .NotEmpty().WithMessage("O campo nome é obrigatório");

            RuleFor(x => x.Telefone)
             .NotNull().WithMessage("O campo telefone é obrigatório")
             .NotEmpty().WithMessage("O campo telefone é obrigatório");

            RuleFor(x => x.Email)
              .NotNull().WithMessage("O campo email é obrigatório")
              .NotEmpty().WithMessage("O campo email é obrigatório");

            RuleFor(x => x.Cidade)
             .NotNull().WithMessage("O campo cidade é obrigatório")
             .NotEmpty().WithMessage("O campo cidade é obrigatório");

            RuleFor(x => x.Estado)
             .NotNull().WithMessage("O campo estado é obrigatório")
             .NotEmpty().WithMessage("O campo estado é obrigatório");
        }
    }
}
