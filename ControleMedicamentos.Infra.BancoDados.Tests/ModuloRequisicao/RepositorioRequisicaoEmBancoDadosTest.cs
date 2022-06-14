using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{
    [TestClass]
    public class RepositorioRequisicaoEmBancoDadosTest
    {
        Requisicao requisicao;
        RepositorioRequisicaoEmBancoDados repositorioRequisicao;
        RepositorioFuncionarioEmBancoDados repositorioFuncionario;
        RepositorioPacienteEmBancoDados repositorioPaciente;
        RepositorioMedicamentoEmBancoDados repositorioMedicamento;

        public RepositorioRequisicaoEmBancoDadosTest()
        {
            Db.ExecutarSql("DELETE FROM TBREQUISICAO; DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)");

            repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();
            repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            repositorioPaciente = new RepositorioPacienteEmBancoDados();
            repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();

            var funcionario = repositorioFuncionario.SelecionarPorId(1);
            var paciente = repositorioPaciente.SelecionarPorId(1);
            var medicamento = repositorioMedicamento.SelecionarPorId(1);

            requisicao = new Requisicao(medicamento, paciente, 5, System.DateTime.Today, funcionario);
        }

        [TestMethod]
        public void Deve_inserir_requisicao()
        {
            //action
            repositorioRequisicao.Inserir(requisicao);

            var requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);

            //assert
            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicao, requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_editar_requisicao()
        {
            //arrange
            repositorioRequisicao.Inserir(requisicao);

            //action
            requisicao.QtdMedicamento = 10;

            repositorioRequisicao.Editar(requisicao);

            var requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);

            //assert
            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicao, requisicaoEncontrada);

        }

        [TestMethod]
        public void Deve_excluir_requisicao()
        {
            //arrange
            repositorioRequisicao.Inserir(requisicao);

            //action 
            repositorioRequisicao.Excluir(requisicao);

            var requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);

            //assert
            Assert.IsNull(requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_uma_requisicao()
        {
            //arrange          
            repositorioRequisicao.Inserir(requisicao);

            //action
            var requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);

            //assert
            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicao, requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_selecionar_todas_as_requisicoes()
        {

            //arrange
            var funcionario = repositorioFuncionario.SelecionarPorId(1);

            var p1 = repositorioPaciente.SelecionarPorId(1);
            var p2 = repositorioPaciente.SelecionarPorId(2);
            var p3 = repositorioPaciente.SelecionarPorId(3);

            var medicamento = repositorioMedicamento.SelecionarPorId(1);

            var r1 = new Requisicao(medicamento, p1, 1, System.DateTime.Today, funcionario);
            var r2 = new Requisicao(medicamento, p2, 2, System.DateTime.Today, funcionario);
            var r3 = new Requisicao(medicamento, p3, 4, System.DateTime.Today, funcionario);

            repositorioRequisicao.Inserir(r1);
            repositorioRequisicao.Inserir(r2);
            repositorioRequisicao.Inserir(r3);

            //action
            var requisicoes = repositorioRequisicao.SelecionarTodos();

            //assert

            Assert.AreEqual(3, requisicoes.Count);

            Assert.AreEqual(r1.Id, requisicoes[0].Id);
            Assert.AreEqual(r2.Id, requisicoes[1].Id);
            Assert.AreEqual(r3.Id, requisicoes[2].Id);

        }

    }
}
