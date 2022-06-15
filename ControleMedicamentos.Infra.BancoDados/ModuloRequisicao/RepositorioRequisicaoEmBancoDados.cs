using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class RepositorioRequisicaoEmBancoDados
    {
        RepositorioFuncionarioEmBancoDados repositorioFuncionario = new();
        RepositorioPacienteEmBancoDados repositorioPaciente = new();
        RepositorioMedicamentoEmBancoDados repositorioMedicamento = new();

        private string enderecoBanco =
         @"Data Source=(LOCALDB)\MSSQLLOCALDB;
              Initial Catalog=ControleMedicamentosDb;
              Integrated Security=True";

        private const string sqlInserir =
         @"INSERT INTO [TBREQUISICAO] 
                (
                    [FUNCIONARIO_ID],
                    [PACIENTE_ID],
                    [MEDICAMENTO_ID],
                    [QUANTIDADEMEDICAMENTO],
                    [DATA]
	            )
	            VALUES
                (
                    @FUNCIONARIO_ID,
                    @PACIENTE_ID,
                    @MEDICAMENTO_ID,
                    @QUANTIDADEMEDICAMENTO,
                    @DATA
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
        @"UPDATE [TBREQUISICAO]	
		        SET
                    [FUNCIONARIO_ID] = @FUNCIONARIO_ID,
                    [PACIENTE_ID] = @PACIENTE_ID,
                    [MEDICAMENTO_ID] = @MEDICAMENTO_ID,
                    [QUANTIDADEMEDICAMENTO] =  @QUANTIDADEMEDICAMENTO,
                    [DATA] =  @DATA
		        WHERE
			        [ID] = @ID";

        private const string sqlExcluir =
        @"DELETE FROM [TBREQUISICAO]			        
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarPorId =
        @"SELECT
                    REQUISICAO.[ID],
                    REQUISICAO.[FUNCIONARIO_ID],
                    REQUISICAO.[PACIENTE_ID],
                    REQUISICAO.[MEDICAMENTO_ID],
                    REQUISICAO.[QUANTIDADEMEDICAMENTO],
                    REQUISICAO.[DATA],
                    
                    FUNCIONARIO.[ID], 
                    PACIENTE.[ID], 
                    MEDICAMENTO.[ID]


                FROM [TBREQUISICAO] AS REQUISICAO
                
                INNER JOIN TBFUNCIONARIO AS FUNCIONARIO
                    ON REQUISICAO.[FUNCIONARIO_ID] = FUNCIONARIO.[ID]

                INNER JOIN TBPACIENTE AS PACIENTE
                    ON REQUISICAO.[PACIENTE_ID] = PACIENTE.[ID]

                INNER JOIN TBMEDICAMENTO AS MEDICAMENTO
                    ON REQUISICAO.[MEDICAMENTO_ID] = MEDICAMENTO.[ID]

                WHERE 
                    REQUISICAO.ID = @ID
            ";

        private const string sqlSelecionarTodos =
         @"SELECT 
                    [ID],
                    [FUNCIONARIO_ID],
                    [PACIENTE_ID],
                    [MEDICAMENTO_ID],
                    [QUANTIDADEMEDICAMENTO],
                    [DATA]
	            FROM 
		            [TBREQUISICAO]";


        private const string sqlAtualizarMedicamento =
         @"UPDATE [TBMEDICAMENTO]	
		        SET
                    [QUANTIDADEDISPONIVEL] = @QUANTIDADEDISPONIVEL
		        WHERE
			        TBMEDICAMENTO.[ID] = @IDMEDICAMENTO";

        public ValidationResult Inserir(Requisicao requisicao)
        {
            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(requisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);


            ConfigurarParametrosRequisicao(requisicao, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            requisicao.Id = Convert.ToInt32(id);

            var medicamentoSelecionado = repositorioMedicamento.SelecionarPorId(requisicao.Medicamento.Id);
            AtualizarMedicamento(requisicao, medicamentoSelecionado);
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Requisicao requisicao)
        {
            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(requisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosRequisicao(requisicao, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public void Excluir(Requisicao requisicao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", requisicao.Id);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();
            conexaoComBanco.Close();
        }

        public Requisicao SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            Requisicao requisicao = null;

            if (leitorRequisicao.Read())
                requisicao = ConverterParaRequisicao(leitorRequisicao);

            conexaoComBanco.Close();

            return requisicao;
        }

        public List<Requisicao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);
            conexaoComBanco.Open();

            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            List<Requisicao> requisicoes = new List<Requisicao>();

            while (leitorRequisicao.Read())
                requisicoes.Add(ConverterParaRequisicao(leitorRequisicao));

            conexaoComBanco.Close();

            return requisicoes;
        }

        public void AtualizarMedicamento(Requisicao requisicao, Medicamento medicamento)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlAtualizarMedicamento, conexaoComBanco);

            comandoEdicao.Parameters.AddWithValue("IDMEDICAMENTO", requisicao.Medicamento.Id);

            comandoEdicao.Parameters.AddWithValue("QUANTIDADEDISPONIVEL", medicamento.QuantidadeDisponivel - requisicao.QtdMedicamento);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

        }


        #region metodos privados

        private void ConfigurarParametrosRequisicao(Requisicao requisicao, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", requisicao.Id);
            comando.Parameters.AddWithValue("FUNCIONARIO_ID", requisicao.Funcionario.Id);
            comando.Parameters.AddWithValue("PACIENTE_ID", requisicao.Paciente.Id);
            comando.Parameters.AddWithValue("MEDICAMENTO_ID", requisicao.Medicamento.Id);
            comando.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO", requisicao.QtdMedicamento);
            comando.Parameters.AddWithValue("DATA", requisicao.Data);

        }

        private Requisicao ConverterParaRequisicao(SqlDataReader leitorRequisicao)
        {
            var id = Convert.ToInt32(leitorRequisicao["ID"]);
            var funcionarioId = Convert.ToInt32(leitorRequisicao["FUNCIONARIO_ID"]);
            var pacienteId = Convert.ToInt32(leitorRequisicao["PACIENTE_ID"]);
            var medicamentoId = Convert.ToInt32(leitorRequisicao["MEDICAMENTO_ID"]);
            var qtdMedicamento = Convert.ToInt32(leitorRequisicao["QUANTIDADEMEDICAMENTO"]);
            var data = Convert.ToDateTime(leitorRequisicao["DATA"]);

            var requisicao = new Requisicao
            {
                Id = id,
                Funcionario = repositorioFuncionario.SelecionarPorId(funcionarioId),
                Paciente = repositorioPaciente.SelecionarPorId(pacienteId),
                Medicamento = repositorioMedicamento.SelecionarPorId(medicamentoId),
                QtdMedicamento = qtdMedicamento,
                Data = data
            };

            return requisicao;
        }
        #endregion
    }
}
