
using ControleMedicamentos.Dominio.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]
    public class ValidadorFornecedorTest
    {
        public ValidadorFornecedorTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var f = new Fornecedor();
            f.Nome = null;

            ValidadorFornecedor validador = new();

            //action
            var resultado = validador.Validate(f);

            //assert
            Assert.AreEqual("O campo nome é obrigatório", resultado.Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void Telefone_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var f = new Fornecedor();
            f.Nome = "Camila";
            f.Telefone = null;

            ValidadorFornecedor validador = new();

            //action
            var resultado = validador.Validate(f);

            //assert
            Assert.AreEqual("O campo telefone é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Email_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var f = new Fornecedor();
            f.Nome = "Camila";
            f.Telefone = "99887766";
            f.Email = null;

            ValidadorFornecedor validador = new();

            //action
            var resultado = validador.Validate(f);

            //assert
            Assert.AreEqual("O campo email é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var f = new Fornecedor();
            f.Nome = "Camila";
            f.Telefone = "99887766";
            f.Email = "email@academiadoprogramador.com";
            f.Cidade = null;

            ValidadorFornecedor validador = new();

            //action
            var resultado = validador.Validate(f);

            //assert
            Assert.AreEqual("O campo cidade é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Estado_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var f = new Fornecedor();
            f.Nome = "Camila";
            f.Telefone = "99887766";
            f.Email = "email@academiadoprogramador.com";
            f.Cidade = "Lages";
            f.Estado = null;

            ValidadorFornecedor validador = new();

            //action
            var resultado = validador.Validate(f);

            //assert
            Assert.AreEqual("O campo estado é obrigatório", resultado.Errors[0].ErrorMessage);
        }
    }
}
