using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {
        private Medicamento medicamento;
        private RepositorioMedicamentoEmBancoDados repositorio;
        private RepositorioFornecedorEmBancoDados repositorioFornecedor;
        public RepositorioMedicamentoEmBancoDadosTest()
        {
            Db.ExecutarSql("DELETE FROM TBMEDICAMENTO; DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)");

            repositorio = new RepositorioMedicamentoEmBancoDados();
            repositorioFornecedor = new RepositorioFornecedorEmBancoDados();

            medicamento = new Medicamento("Cloridrato de metformina 500mg", "Lorem Ipsum is simply", "8854AE", new DateTime(2026, 07, 02), 12);
            medicamento.Fornecedor = repositorioFornecedor.SelecionarPorId(1);

        }

        [TestMethod]
        public void Deve_inserir_medicamento()
        {
            //action
            repositorio.Inserir(medicamento);

            //assert
            var medicamentoEncontrado = repositorio.SelecionarPorId(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento, medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_editar_medicamento()
        {
            //arrange
            repositorio.Inserir(medicamento);


            //action
            medicamento.QuantidadeDisponivel = 100;
            repositorio.Editar(medicamento);

            //assert
            var medicamentoEncontrado = repositorio.SelecionarPorId(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento, medicamentoEncontrado);

        }

        [TestMethod]
        public void Deve_excluir_medicamento()
        {
            //arrange 
            repositorio.Inserir(medicamento);

            //action
            repositorio.Excluir(medicamento);

            var medicamentoEncontrado = repositorio.SelecionarPorId(medicamento.Id);

            //assert
            Assert.IsNull(medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_um_medicamento()
        {
            //arrange          
            repositorio.Inserir(medicamento);

            //action
            var medicamentoEncontrado = repositorio.SelecionarPorId(medicamento.Id);

            //assert
            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento, medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_medicamentos()
        {
            //arrange
            var m1 = new Medicamento("Cloridrato de metformina 500mg", "Lorem Ipsum is simply", "999PSQ", new DateTime(2027, 05, 02), 202);
            m1.Fornecedor = repositorioFornecedor.SelecionarPorId(1);
            var m2 = new Medicamento("Captopril 25mg", "Lorem Ipsum is simply", "888635", new DateTime(2024, 08, 09), 300);
            m2.Fornecedor = repositorioFornecedor.SelecionarPorId(1); 
            var m3 = new Medicamento("Losartana Potássica 50mg", "Lorem Ipsum is simply", "452LDS", new DateTime(2026, 04, 04), 50);
            m3.Fornecedor = repositorioFornecedor.SelecionarPorId(1);

            repositorio.Inserir(m1);
            repositorio.Inserir(m2);
            repositorio.Inserir(m3);

            //action
            var medicamentos = repositorio.SelecionarTodos();

            //assert

            Assert.AreEqual(3, medicamentos.Count);

            Assert.AreEqual(m1.Id, medicamentos[0].Id);
            Assert.AreEqual(m2.Id, medicamentos[1].Id);
            Assert.AreEqual(m3.Id, medicamentos[2].Id);

        }
    }
}
