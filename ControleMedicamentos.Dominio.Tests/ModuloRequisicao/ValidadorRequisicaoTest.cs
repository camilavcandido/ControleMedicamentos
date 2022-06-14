

using ControleMedicamentos.Dominio.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloRequisicao
{
    [TestClass]
    public class ValidadorRequisicaoTest
    {
        public ValidadorRequisicaoTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");

        }

        [TestMethod]
        public void Medicamento_da_requisicao_deve_ser_obrigatorio()
        {
            //arrange
            var r = new Requisicao();
            r.Medicamento = null;

            ValidadorRequisicao validador = new();

            //action
            var resultado = validador.Validate(r);

            //assert
            Assert.AreEqual("O campo medicamento é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Paciente_da_requisicao_deve_ser_obrigatorio()
        {
            //arrange
            var r = new Requisicao();
            r.Medicamento = new Dominio.ModuloMedicamento.Medicamento();
            r.Paciente = null;

            ValidadorRequisicao validador = new();

            //action
            var resultado = validador.Validate(r);

            //assert
            Assert.AreEqual("O campo paciente é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Quantidade_de_medicamentos_da_requisicao_deve_ser_obrigatorio()
        {
            //arrange
            var r = new Requisicao();
            r.Medicamento = new Dominio.ModuloMedicamento.Medicamento();
            r.Paciente = new Dominio.ModuloPaciente.Paciente();
            r.QtdMedicamento = 0;

            ValidadorRequisicao validador = new();

            //action
            var resultado = validador.Validate(r);

            //assert
            Assert.AreEqual("O campo quantidade de medicamento é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Data_da_requisicao_deve_ser_obrigatorio()
        {
            //arrange
            var r = new Requisicao();
            r.Medicamento = new Dominio.ModuloMedicamento.Medicamento();
            r.Paciente = new Dominio.ModuloPaciente.Paciente();
            r.QtdMedicamento = 2;
            r.Data = System.DateTime.MinValue;

            ValidadorRequisicao validador = new();

            //action
            var resultado = validador.Validate(r);

            //assert
            Assert.AreEqual("O campo data é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Funcionario_da_requisicao_deve_ser_obrigatorio()
        {
            //arrange
            var r = new Requisicao();
            r.Medicamento = new Dominio.ModuloMedicamento.Medicamento();
            r.Paciente = new Dominio.ModuloPaciente.Paciente();
            r.QtdMedicamento = 2;
            r.Data = System.DateTime.Now;
            r.Funcionario = null;

            ValidadorRequisicao validador = new();

            //action
            var resultado = validador.Validate(r);

            //assert
            Assert.AreEqual("O campo funcionário é obrigatório", resultado.Errors[0].ErrorMessage);
        }
    }
}
