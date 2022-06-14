using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class Medicamento : EntidadeBase<Medicamento>
    {        
        public Medicamento()
        {

        }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Lote { get; set; }
        public DateTime Validade { get; set; }
        public int QuantidadeDisponivel { get; set; }

        public List<Requisicao> Requisicoes { get; set; }

        public Fornecedor Fornecedor{ get; set; }

        public int QuantidadeRequisicoes { get { return Requisicoes.Count; } }

        public Medicamento(string nome, string descricao, string lote, DateTime validade, int qtdDisponivel /*Fornecedor f*/)
        {
            Nome = nome;
            Descricao = descricao;
            Lote = lote;
            Validade = validade;
            QuantidadeDisponivel = qtdDisponivel;
            //Fornecedor = f;
           // Requisicoes = new List<Requisicao>();
        }

        public override bool Equals(object obj)
        {
            Medicamento m = obj as Medicamento;

            if (m == null)
                return false;

            return
                m.Id.Equals(Id) &&
                m.Nome.Equals(Nome) &&
                m.Descricao.Equals(Descricao) &&
                m.Lote.Equals(Lote) &&
                m.Validade.Equals(Validade) &&
                m.QuantidadeDisponivel.Equals(QuantidadeDisponivel) &&
                m.Fornecedor.Id.Equals(Fornecedor.Id);


        }
    }
}
