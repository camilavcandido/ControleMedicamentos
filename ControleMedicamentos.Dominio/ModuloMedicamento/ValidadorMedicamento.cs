using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class ValidadorMedicamento : AbstractValidator<Medicamento>
    {
        public ValidadorMedicamento()
        {
            RuleFor(x => x.Nome)
            .NotNull().WithMessage("O campo nome é obrigatório")
            .NotEmpty().WithMessage("O campo nome é obrigatório");

            RuleFor(x => x.Descricao)
            .NotNull().WithMessage("O campo descrição é obrigatório")
            .NotEmpty().WithMessage("O campo descrição é obrigatório");

            RuleFor(x => x.Lote)
            .NotNull().WithMessage("O campo lote é obrigatório")
            .NotEmpty().WithMessage("O campo lote é obrigatório");

            RuleFor(x => x.Validade)
            .NotNull().WithMessage("O campo validade é obrigatório")
            .NotEmpty().WithMessage("O campo validade é obrigatório");

            RuleFor(x => x.Fornecedor)
            .NotNull().WithMessage("O campo fornecedor é obrigatório")
            .NotEmpty().WithMessage("O campo fornecedor é obrigatório");
        }
    }
}
