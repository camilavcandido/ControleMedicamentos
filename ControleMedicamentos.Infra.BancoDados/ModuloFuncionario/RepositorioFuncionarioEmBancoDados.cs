using ControleMedicamentos.Dominio.ModuloFuncionario;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace ControleMedicamentos.Infra.BancoDados.ModuloFuncionario
{
    public class RepositorioFuncionarioEmBancoDados
    {
        private string enderecoBanco =
           @"Data Source=(LOCALDB)\MSSQLLOCALDB;
              Initial Catalog=ControleMedicamentosDb;
              Integrated Security=True";

        #region SQL Queries
        private const string sqlInserir =
        @"INSERT INTO [TBFUNCIONARIO] 
                (
                    [NOME],
                    [LOGIN],
                    [SENHA]
	            )
	            VALUES
                (
                    @NOME,
                    @LOGIN,
                    @SENHA
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
         @"UPDATE [TBFUNCIONARIO]	
		        SET
			        [NOME] = @NOME,
			        [LOGIN] = @LOGIN,
                    [SENHA] = @SENHA
		        WHERE
			        [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBFUNCIONARIO]
                WHERE
                    [ID] = @ID";

        private const string sqlSelecionarPorId =
            @"SELECT
	                [ID],
                    [NOME],
                    [LOGIN],
                    [SENHA]
              FROM 
	                [TBFUNCIONARIO]
              WHERE 
	                [ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT 
                   [ID],
                   [NOME],
                   [LOGIN],
                   [SENHA]
            FROM [TBFUNCIONARIO]";

        #endregion


        public ValidationResult Inserir(Funcionario funcionario)
        {
            var validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosFuncionario(funcionario, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            funcionario.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Funcionario funcionario)
        {
            var validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosFuncionario(funcionario, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public void Excluir(Funcionario funcionario)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", funcionario.Id);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();
            conexaoComBanco.Close();
        }

        public Funcionario SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorFuncionario = comandoSelecao.ExecuteReader();

            Funcionario funcionario = null;

            if (leitorFuncionario.Read())
                funcionario = ConverterParaFuncionario(leitorFuncionario);

            conexaoComBanco.Close();

            return funcionario;
        }

        public List<Funcionario> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();

            SqlDataReader leitorFuncionario = comandoSelecao.ExecuteReader();

            List<Funcionario> funcionarios = new List<Funcionario>();

            while(leitorFuncionario.Read())
                funcionarios.Add(ConverterParaFuncionario(leitorFuncionario));

            conexaoComBanco.Close();

            return funcionarios;

        }

        #region metodos privados
        private void ConfigurarParametrosFuncionario(Funcionario registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("NOME", registro.Nome);
            comando.Parameters.AddWithValue("LOGIN", registro.Login);
            comando.Parameters.AddWithValue("SENHA", registro.Senha);

        }
        private Funcionario ConverterParaFuncionario(SqlDataReader leitorFuncionario)
        {
            var id = Convert.ToInt32(leitorFuncionario["ID"]);
            var nome = Convert.ToString(leitorFuncionario["NOME"]);
            var login = Convert.ToString(leitorFuncionario["LOGIN"]);
            var senha = Convert.ToString(leitorFuncionario["SENHA"]);

            var f = new Funcionario()
            {
                Id = id,
                Nome = nome,
                Login = login,
                Senha = senha

            };

            return f;
        }

        #endregion
    }
}
