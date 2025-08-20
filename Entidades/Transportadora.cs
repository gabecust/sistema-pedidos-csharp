
using ProjetoLoja.Entidades;

namespace ProjetoLoja.Models
{
    public class Transportadora : EntidadeBase
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public double ValorFrete { get; set; }

        public Transportadora(string nome, string telefone, double valorFrete)
        {
            Nome = nome;
            Telefone = telefone;
            ValorFrete = valorFrete;
        }

        public override string ToString()
        {
            return $"ID: {Id} - {Nome} - Tel: {Telefone} - Frete: {ValorFrete:C}";
        }
    }
}
