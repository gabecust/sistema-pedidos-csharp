using ProjetoLoja.Entidades;

namespace ProjetoLoja.Models
{
    public class Produto : EntidadeBase
    {
        public string Nome { get; set; }
        public double Preco { get; set; }
        public int Estoque { get; set; }
        public string Descricao { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public Produto(string nome, string descricao, int estoque, double preco, Fornecedor fornecedor)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Estoque = estoque;
            Fornecedor = fornecedor;
        }

        public override string ToString()
        {
            return $"ID: {Id} - {Nome} - {Descricao} - R$ {Preco:F2} - Estoque: {Estoque} - Fornecedor: {Fornecedor.Nome}";
        }
    }
}
