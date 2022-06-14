using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class MedicamentoTest
    {
        [TestMethod]
        public void Deve_atualizar_quantidade_de_requisicoes_do_medicamento()
        {
            //arrange
            var m = new Medicamento();
            m.Nome = "Cloridrato de metformina 500mg";
            m.Descricao = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. ";
            m.Lote = "8854AE";
            m.Validade = new DateTime(2024, 05, 02);
            m.Fornecedor = new Dominio.ModuloFornecedor.Fornecedor();

            var req1 = new Requisicao();
            req1.Medicamento = m;

            var req2 = new Requisicao();
            req2.Medicamento = m;

            var requisicoes = new List<Requisicao>();
            requisicoes.Add(req1);
            requisicoes.Add(req2);

            //action
            m.Requisicoes = requisicoes;

            //assert
            Assert.AreEqual(2, m.QuantidadeRequisicoes);
        }
    }
}
