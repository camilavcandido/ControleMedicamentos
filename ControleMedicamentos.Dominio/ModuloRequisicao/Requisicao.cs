using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using System;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class Requisicao : EntidadeBase<Requisicao>
    {   

        public Medicamento Medicamento { get; set; }
        public Paciente Paciente { get; set; }
        public int QtdMedicamento { get; set; }
        public DateTime Data { get; set; }
        public Funcionario Funcionario { get; set; }

        public Requisicao()
        {

        }

        public Requisicao(Medicamento medicamento, Paciente paciente, int qtdMedicamento, DateTime data, Funcionario funcionario)
        {
            Medicamento = medicamento;
            Paciente = paciente;
            QtdMedicamento = qtdMedicamento;
            Data = data;
            Funcionario = funcionario;
        }

        public override bool Equals(object obj)
        {
            Requisicao r = obj as Requisicao;

            if (r == null)
                return false;

            return
                r.Id.Equals(Id) &&
                r.Medicamento.Id.Equals(Medicamento.Id) &&
                r.Paciente.Id.Equals(Paciente.Id) && 
                r.QtdMedicamento.Equals(QtdMedicamento) &&
                r.Data.Equals(Data) &&
                r.Funcionario.Id.Equals(Funcionario.Id);
        }
    }
}
