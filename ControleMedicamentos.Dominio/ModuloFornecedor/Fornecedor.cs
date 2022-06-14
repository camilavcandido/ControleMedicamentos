namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public class Fornecedor : EntidadeBase<Fornecedor>
    {
        public Fornecedor(string nome, string telefone, string email, string cidade, string estado) : this()
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
            Cidade = cidade;
            Estado = estado;
        }

        public Fornecedor()
        {

        }

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public override bool Equals(object obj)
        {
            Fornecedor f = obj as Fornecedor;

            if (f == null)
                return false;

            return
                f.Id.Equals(Id) &&
                f.Nome.Equals(Nome) &&
                f.Telefone.Equals(Telefone) &&
                f.Email.Equals(Email) &&
                f.Cidade.Equals(Cidade) &&
                f.Estado.Equals(Estado);

        }
    }
}
