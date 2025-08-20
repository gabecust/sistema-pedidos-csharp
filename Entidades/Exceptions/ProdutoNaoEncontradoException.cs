using System;

namespace ProjetoLoja.Entidades.Exceptions
{
    public class ProdutoNaoEncontradoException : Exception
    {
        public int CodigoProduto { get; }

        public ProdutoNaoEncontradoException(int codigo)
            : base($"Produto com código {codigo} não foi encontrado.")
        {
            CodigoProduto = codigo;
        }

        public ProdutoNaoEncontradoException(string mensagem)
            : base(mensagem)
        {
        }
    }
}
