using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class ValidadorPaciente : AbstractValidator<Paciente>
    {
        public ValidadorPaciente()
        {
            RuleFor(x => x.Nome)
                 .NotNull().WithMessage("O campo nome é obrigatório")
                .NotEmpty().WithMessage("O campo nome é obrigatório");

            RuleFor(x => x.CartaoSUS)
                .NotNull().WithMessage("O campo cartão SUS é obrigatório")
                .NotEmpty().WithMessage("O campo cartão SUS é obrigatório");
        }
    }
}
