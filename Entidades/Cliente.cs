using ProjetoLoja.Entidades;

namespace ProjetoLoja.Models
{
    public class Cliente : Usuario
    {
        public string NomeCompleto { get; set; }
        public Endereco Endereco { get; set; }

        public Cliente(string nomeUsuario, string senha, string nomeCompleto, Endereco endereco)
            : base(nomeUsuario, senha)
        {
            NomeCompleto = nomeCompleto;
            Endereco = endereco;
        }

        public override string Tipo => "Cliente";

        public override string ToString()
        {
            return $"ID: {Id} - {NomeCompleto} - Usuário: {NomeUsuario} - Endereço: {Endereco}";
        }
    }
}
