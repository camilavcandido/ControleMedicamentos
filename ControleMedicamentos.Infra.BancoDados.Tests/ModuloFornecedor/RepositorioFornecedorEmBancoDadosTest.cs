using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{
    [TestClass]
    public class RepositorioFornecedorEmBancoDadosTest
    {
        private Fornecedor fornecedor;
        private RepositorioFornecedorEmBancoDados repositorio;

        public RepositorioFornecedorEmBancoDadosTest()
        {
            Db.ExecutarSql("DELETE FROM TBFORNECEDOR; DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)");

            fornecedor = new Fornecedor("Alexandre Rech", "4999828282", "rech@academiadoprogramador.com", "Lages", "SC");
            repositorio = new RepositorioFornecedorEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_fornecedor()
        {
            //action
            repositorio.Inserir(fornecedor);

            var resultado = repositorio.SelecionarPorId(fornecedor.Id);

            //assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(fornecedor, resultado);

        }

        [TestMethod]
        public void Deve_editar_fornecedor()
        {
            //arrange
            repositorio.Inserir(fornecedor);

            //action
            fornecedor.Nome = "Tiago Santini";
            fornecedor.Telefone = "4999632574";
            fornecedor.Email = "santini@academiadoprogramador.com";
            fornecedor.Cidade = "Lages";
            fornecedor.Estado = "SC";

            repositorio.Editar(fornecedor);

            //assert
            var fornecedorEncontrado = repositorio.SelecionarPorId(fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor, fornecedorEncontrado);


        }

        [TestMethod]
        public void Deve_excluir_fornecedor()
        {
            //arrange
            repositorio.Inserir(fornecedor);

            //action
            repositorio.Excluir(fornecedor);

            var fornecedorEncontrado = repositorio.SelecionarPorId(fornecedor.Id);

            //assert
            Assert.IsNull(fornecedorEncontrado);

        }

        [TestMethod]
        public void Deve_selecionar_um_fornecedor()
        {
            //arrange
            repositorio.Inserir(fornecedor);

            //action
            var fornecedorEncontrado = repositorio.SelecionarPorId(fornecedor.Id);

            //assert
            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor, fornecedorEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_fornecedores()
        {
            //arrange
            var f1 = new Fornecedor("Fornecedor Um", "4999778844", "f1@email.com", "Lages", "SC");
            var f2 = new Fornecedor("Fornecedor Dois", "1199748884", "f2@email.com", "São Paulo", "SP");
            var f3 = new Fornecedor("Fornecedor Três", "4999778844", "f3@email.com", "Lages", "SC");

            repositorio.Inserir(f1);
            repositorio.Inserir(f2);
            repositorio.Inserir(f3);

            //action
            List<Fornecedor> fornecedores = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, fornecedores.Count);
            Assert.AreEqual(fornecedores[0].Id, f1.Id);
            Assert.AreEqual(fornecedores[1].Id, f2.Id);
            Assert.AreEqual(fornecedores[2].Id, f3.Id);

        }
    }
}
