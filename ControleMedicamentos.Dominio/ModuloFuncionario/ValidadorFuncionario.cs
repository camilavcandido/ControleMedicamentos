

using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloFuncionario
{
    public class ValidadorFuncionario : AbstractValidator<Funcionario>
    {
        public ValidadorFuncionario()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("O campo nome é obrigatório")
                .NotEmpty().WithMessage("O campo nome é obrigatório");

            RuleFor(x => x.Login)
                .NotNull().WithMessage("O campo login é obrigatório")
                .NotEmpty().WithMessage("O campo login é obrigatório");

            RuleFor(x => x.Senha)
                .NotNull().WithMessage("O campo senha é obrigatório")
                .NotEmpty().WithMessage("O campo senha é obrigatório")
                .MinimumLength(8).WithMessage("A senha deve ter no minímo 8 (oito) caracteres");

        }
    }
}
