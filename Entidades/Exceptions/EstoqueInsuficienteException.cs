namespace ProjetoLoja.Entidades.Exceptions
{
    public class EstoqueInsuficienteException : Exception
    {
        public int EstoqueDisponivel { get; }
        public int QuantidadeSolicitada { get; }
        public string NomeProduto { get; }

        public EstoqueInsuficienteException(string nomeProduto, int quantidadeSolicitada, int estoqueDisponivel)
            : base($"Produto '{nomeProduto}': Estoque insuficiente. Solicitado: {quantidadeSolicitada}, Disponível: {estoqueDisponivel}")
        {
            NomeProduto = nomeProduto;
            QuantidadeSolicitada = quantidadeSolicitada;
            EstoqueDisponivel = estoqueDisponivel;
        }
    }
}
