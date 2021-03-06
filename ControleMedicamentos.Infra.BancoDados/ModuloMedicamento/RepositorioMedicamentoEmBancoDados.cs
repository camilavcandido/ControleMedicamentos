using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamento.Infra.BancoDados.ModuloMedicamento
{
    public class RepositorioMedicamentoEmBancoDados
    {
        RepositorioFornecedorEmBancoDados  repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
        private string enderecoBanco =
        @"Data Source=(LOCALDB)\MSSQLLOCALDB;
              Initial Catalog=ControleMedicamentosDb;
              Integrated Security=True";

        #region SQL Queries
        private const string sqlInserir =
            @"INSERT INTO [TBMEDICAMENTO]
            (
            [NOME],
            [DESCRICAO],
            [LOTE],
            [VALIDADE],
            [QUANTIDADEDISPONIVEL],
            [FORNECEDOR_ID]
            )
	        VALUES
             (
                @NOME,
                @DESCRICAO,
                @LOTE,
                @VALIDADE,
                @QUANTIDADEDISPONIVEL,
                @FORNECEDOR_ID
             );SELECT SCOPE_IDENTITY(); ";

        private const string sqlEditar =
         @"UPDATE [TBMEDICAMENTO]	
		        SET
                    [NOME] = @NOME,
                    [DESCRICAO] = @DESCRICAO,
                    [LOTE] = @LOTE,
                    [VALIDADE] = @VALIDADE,
                    [QUANTIDADEDISPONIVEL] = @QUANTIDADEDISPONIVEL,
                    [FORNECEDOR_ID] = @FORNECEDOR_ID
		        WHERE
			        [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBMEDICAMENTO]
                WHERE
                [ID] = @ID";

        private const string sqlSelecionarPorId =
            @"SELECT
	            M.[ID],
                M.[NOME],
                M.[DESCRICAO],
                M.[LOTE],
                M.[VALIDADE],
                M.[QUANTIDADEDISPONIVEL],
                M.[FORNECEDOR_ID],
                F.[ID]
                    FROM 
                [TBMEDICAMENTO] AS M
                INNER JOIN [TBFORNECEDOR] AS F
                 ON M.FORNECEDOR_ID = F.ID

                 WHERE M.ID = @ID";

        private const string sqlSelecionarTodos =
        @"SELECT 
		            [ID], 
                    [NOME],
                    [DESCRICAO],
                    [LOTE],
                    [VALIDADE],
                    [QUANTIDADEDISPONIVEL],
                    [FORNECEDOR_ID]
	            FROM 
		            [TBMEDICAMENTO]";


        private const string sqlSelecionarTodosComBaixaQuantidade =
        @"SELECT 
		            [ID], 
                    [NOME],
                    [DESCRICAO],
                    [LOTE],
                    [VALIDADE],
                    [QUANTIDADEDISPONIVEL],
                    [FORNECEDOR_ID]
	            FROM 
		            [TBMEDICAMENTO]
            WHERE QUANTIDADEDISPONIVEL <= 10
                ";
        #endregion

        public ValidationResult Inserir(Medicamento medicamento)
        {
            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);


            ConfigurarParametrosMedicamento(medicamento, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            medicamento.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Medicamento medicamento)
        {
            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);


            ConfigurarParametrosMedicamento(medicamento, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;

        }

        public void Excluir(Medicamento medicamento)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);
            
            comandoExclusao.Parameters.AddWithValue("ID", medicamento.Id);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();
            conexaoComBanco.Close();
        }
        public Medicamento SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            Medicamento medicamento = null;

            if (leitorMedicamento.Read())
                medicamento = ConverterParaMedicamento(leitorMedicamento);

            conexaoComBanco.Close();

            return medicamento;
        }

        public List<Medicamento> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);
            conexaoComBanco.Open();

            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            List<Medicamento> medicamentos = new List<Medicamento>();

            while (leitorMedicamento.Read())
                medicamentos.Add(ConverterParaMedicamento(leitorMedicamento));

            conexaoComBanco.Close();

            return medicamentos;
        }

        public List<Medicamento> SelecionarTodosComBaixaQuantidade()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodosComBaixaQuantidade, conexaoComBanco);
            conexaoComBanco.Open();

            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            List<Medicamento> medicamentos = new List<Medicamento>();

            while (leitorMedicamento.Read())
                medicamentos.Add(ConverterParaMedicamento(leitorMedicamento));

            conexaoComBanco.Close();

            return medicamentos;
        }

        #region metodos privados
        private void ConfigurarParametrosMedicamento(Medicamento medicamento, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", medicamento.Id);
            comando.Parameters.AddWithValue("NOME", medicamento.Nome);
            comando.Parameters.AddWithValue("DESCRICAO", medicamento.Descricao);
            comando.Parameters.AddWithValue("LOTE", medicamento.Lote);
            comando.Parameters.AddWithValue("VALIDADE", medicamento.Validade);
            comando.Parameters.AddWithValue("QUANTIDADEDISPONIVEL", medicamento.QuantidadeDisponivel);
            comando.Parameters.AddWithValue("FORNECEDOR_ID", medicamento.Fornecedor.Id);
        }
        private Medicamento ConverterParaMedicamento(SqlDataReader leitorMedicamento)
        {
            var id = Convert.ToInt32(leitorMedicamento["ID"]);
            var nome = Convert.ToString(leitorMedicamento["NOME"]);
            var descricao = Convert.ToString(leitorMedicamento["DESCRICAO"]);
            var lote = Convert.ToString(leitorMedicamento["LOTE"]);
            var validade = Convert.ToDateTime(leitorMedicamento["VALIDADE"]);
            var qtdDisponivel = Convert.ToInt32(leitorMedicamento["QUANTIDADEDISPONIVEL"]);
            var fornecedor_id = Convert.ToInt32(leitorMedicamento["FORNECEDOR_ID"]);

            var m = new Medicamento
            {
                Id = id,
                Nome = nome,
                Descricao = descricao,
                Lote = lote,
                Validade = validade,
                QuantidadeDisponivel = qtdDisponivel,

                Fornecedor = repositorioFornecedor.SelecionarPorId(fornecedor_id)

            };

            return m;
        }


        #endregion
    }
}
