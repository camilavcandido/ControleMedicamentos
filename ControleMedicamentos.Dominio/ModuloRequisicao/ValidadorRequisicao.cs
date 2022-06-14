
using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {
        public ValidadorRequisicao()
        {
            RuleFor(x => x.Medicamento)
               .NotNull().WithMessage("O campo medicamento é obrigatório")
               .NotEmpty().WithMessage("O campo medicamento é obrigatório");

            RuleFor(x => x.Paciente)
             .NotNull().WithMessage("O campo paciente é obrigatório")
             .NotEmpty().WithMessage("O campo paciente é obrigatório");

            RuleFor(x => x.QtdMedicamento)
             .NotNull().WithMessage("O campo quantidade de medicamento é obrigatório")
             .NotEmpty().WithMessage("O campo quantidade de medicamento é obrigatório");

            RuleFor(x => x.Data)
              .NotNull().WithMessage("O campo data é obrigatório")
              .NotEmpty().WithMessage("O campo data é obrigatório");

            RuleFor(x => x.Funcionario)
              .NotNull().WithMessage("O campo funcionário é obrigatório")
              .NotEmpty().WithMessage("O campo funcionário é obrigatório");

        }

    }
}
