

using ControleMedicamentos.Dominio.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloFuncionario
{
    [TestClass]
    public class ValidadorFuncionarioTest
    {
        public ValidadorFuncionarioTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_do_funcionario_deve_ser_obrigatorio()
        {
            //arrange
            var funcionario = new Funcionario();
            funcionario.Nome = null;

            ValidadorFuncionario validador = new();

            //action
            var resultado = validador.Validate(funcionario);

            //assert
            Assert.AreEqual("O campo nome é obrigatório", resultado.Errors[0].ErrorMessage);

        }

        [TestMethod]
        public void Login_do_funcionario_deve_ser_obrigatorio()
        {
            //arrange
            var funcionario = new Funcionario();
            funcionario.Nome = "Camila";
            funcionario.Login = null;


            ValidadorFuncionario validador = new();

            //action
            var resultado = validador.Validate(funcionario);

            //assert
            Assert.AreEqual("O campo login é obrigatório", resultado.Errors[0].ErrorMessage);

        }

        [TestMethod]
        public void Senha_do_funcionario_deve_ser_obrigatorio()
        {
            //arrange
            var funcionario = new Funcionario();
            funcionario.Nome = "Camila";
            funcionario.Login = "camilaacadprogramdor";
            funcionario.Senha = null;

            ValidadorFuncionario validador = new();

            //action
            var resultado = validador.Validate(funcionario);

            //assert
            Assert.AreEqual("O campo senha é obrigatório", resultado.Errors[0].ErrorMessage);

        }

        [TestMethod]
        public void Senha_do_funcionario_deve_ter_no_minino_oito_caracteres()
        {
            //arrange
            var funcionario = new Funcionario();
            funcionario.Nome = "Camila";
            funcionario.Login = "camilaacadprogramdor";
            funcionario.Senha = "1";

            ValidadorFuncionario validador = new();

            //action
            var resultado = validador.Validate(funcionario);

            //assert
            Assert.AreEqual("A senha deve ter no minímo 8 (oito) caracteres", resultado.Errors[0].ErrorMessage);

        }
    }
}
