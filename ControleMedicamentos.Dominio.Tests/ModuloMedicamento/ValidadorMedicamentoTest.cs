
using ControleMedicamentos.Dominio.ModuloMedicamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class ValidadorMedicamentoTest
    {
        public ValidadorMedicamentoTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_do_medicamento_deve_ser_obrigatorio()
        {
            //arrange
            var m = new Medicamento();
            m.Nome = null;

            ValidadorMedicamento validador = new();

            //action
            var resultado = validador.Validate(m);

            //assert
            Assert.AreEqual("O campo nome é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Descricao_do_medicamento_deve_ser_obrigatorio()
        {
            //arrange
            var m = new Medicamento();
            m.Nome = "Cloridrato de metformina 500mg";
            m.Descricao = null;


            ValidadorMedicamento validador = new();

            //action
            var resultado = validador.Validate(m);

            //assert
            Assert.AreEqual("O campo descrição é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Lote_do_medicamento_deve_ser_obrigatorio()
        {
            //arrange
            var m = new Medicamento();
            m.Nome = "Cloridrato de metformina 500mg";
            m.Descricao = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. ";
            m.Lote = null;

            ValidadorMedicamento validador = new();

            //action
            var resultado = validador.Validate(m);

            //assert
            Assert.AreEqual("O campo lote é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Validade_do_medicamento_deve_ser_obrigatorio()
        {
            //arrange
            var m = new Medicamento();
            m.Nome = "Cloridrato de metformina 500mg";
            m.Descricao = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. ";
            m.Lote = "8854AE";
            m.Validade = System.DateTime.MinValue;

            ValidadorMedicamento validador = new();

            //action
            var resultado = validador.Validate(m);

            //assert
            Assert.AreEqual("O campo validade é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Fornecedor_do_medicamento_deve_ser_obrigatorio()
        {
            //arrange
            var m = new Medicamento();
            m.Nome = "Cloridrato de metformina 500mg";
            m.Descricao = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. ";
            m.Lote = "8854AE";
            m.Validade = new DateTime(2024, 05, 02);
            m.Fornecedor = null;

            ValidadorMedicamento validador = new();

            //action
            var resultado = validador.Validate(m);

            //assert
            Assert.AreEqual("O campo fornecedor é obrigatório", resultado.Errors[0].ErrorMessage);
        }


    }
}
