using ProjetoLoja.Entidades;

namespace ProjetoLoja.Models
{
    public class Fornecedor : EntidadeBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public Endereco Endereco { get; set; }

        public Fornecedor(string nome, string descricao, string telefone, string email, Endereco endereco)
        {
            Nome = nome;
            Descricao = descricao;
            Telefone = telefone;
            Email = email;
            Endereco = endereco;
        }

        public override string ToString()
        {
            return $"ID: {Id} - {Nome} - {Descricao} - Tel: {Telefone} - Email: {Email}";
        }
    }
}
