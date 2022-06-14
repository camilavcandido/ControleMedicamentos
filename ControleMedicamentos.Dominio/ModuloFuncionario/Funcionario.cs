namespace ControleMedicamentos.Dominio.ModuloFuncionario
{
    public class Funcionario : EntidadeBase<Funcionario>
    {

        public Funcionario(string nome, string login, string senha) : this()
        {
            Nome = nome;
            Login = login;
            Senha = senha;
        }

        public Funcionario()
        {

        }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public override bool Equals(object obj)
        {
            Funcionario f = obj as Funcionario;

            if (f == null)
                return false;

            return
                f.Id.Equals(Id) &&
                f.Nome.Equals(Nome) &&
                f.Login.Equals(Login) &&
                f.Senha.Equals(Senha);
        }
    }
}
