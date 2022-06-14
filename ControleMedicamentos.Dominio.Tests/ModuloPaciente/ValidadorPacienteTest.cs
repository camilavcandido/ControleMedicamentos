using ControleMedicamentos.Dominio.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloPaciente
{
    [TestClass]
    public class ValidadorPacienteTest
    {
        public ValidadorPacienteTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }
        [TestMethod]
        public void Nome_do_paciente_deve_ser_obrigatorio()
        {
            //arrange
            var p = new Paciente();
            p.Nome = null;

            ValidadorPaciente validador = new ValidadorPaciente();

            //action
            var resultado = validador.Validate(p);

            //assert
            Assert.AreEqual("O campo nome é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cartao_SUS_deve_ser_obrigatorio()
        {
            //arrange
            var p = new Paciente();
            p.Nome = "Camila";
            p.CartaoSUS = null;
            ValidadorPaciente validador = new ValidadorPaciente();

            //action
            var resultado = validador.Validate(p);

            //assert
            Assert.AreEqual("O campo cartão SUS é obrigatório", resultado.Errors[0].ErrorMessage);

        }
    }
}
