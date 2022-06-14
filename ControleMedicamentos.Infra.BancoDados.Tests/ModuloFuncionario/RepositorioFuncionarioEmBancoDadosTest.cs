using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioEmBancoDadosTest
    {
        private Funcionario funcionario;
        private RepositorioFuncionarioEmBancoDados repositorio;

        public RepositorioFuncionarioEmBancoDadosTest()
        {
            Db.ExecutarSql("DELETE FROM TBFUNCIONARIO; DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)");

            funcionario = new Funcionario("Maria Clara", "mclara", "77441188");
            repositorio = new RepositorioFuncionarioEmBancoDados();
        }


        [TestMethod]
        public void Deve_inserir_funcionario()
        {
            //action
            repositorio.Inserir(funcionario);

            //assert
            var funcionarioEncontrado = repositorio.SelecionarPorId(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario, funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_editar_funcionario()
        {
            //arrange                      
            repositorio.Inserir(funcionario);

            //action
            funcionario.Nome = "Maria Clara dos Santos";
            repositorio.Editar(funcionario);

            //assert
            var funcionarioEncontrado = repositorio.SelecionarPorId(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario, funcionarioEncontrado);

        }

        [TestMethod]
        public void Deve_excluir_funcionario()
        {
            //arrange
            repositorio.Inserir(funcionario);

            //action
            repositorio.Excluir(funcionario);

            //assert
            var funcionarioEncontrado = repositorio.SelecionarPorId(funcionario.Id);

            Assert.IsNull(funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_um_funcionario()
        {
            //arrange          
            repositorio.Inserir(funcionario);

            //action
            var funcionarioEncontrado = repositorio.SelecionarPorId(funcionario.Id);

            //assert
            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario, funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_funcionarios()
        {
            //arrange
            var f1 = new Funcionario("Ana", "anajulia", "77445522");
            var f2 = new Funcionario("Bruno", "brunoramos", "99663322");
            var f3 = new Funcionario("Carlos", "carlosferreira", "11112222");

            repositorio.Inserir(f1);
            repositorio.Inserir(f2);
            repositorio.Inserir(f3);

            //action

            List<Funcionario> funcionarios = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, funcionarios.Count);
            Assert.AreEqual(f1.Id, funcionarios[0].Id);
            Assert.AreEqual(f2.Id, funcionarios[1].Id);
            Assert.AreEqual(f3.Id, funcionarios[2].Id);
        }
    }

    
}
