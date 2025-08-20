using System;
using ProjetoLoja.Models;

namespace ProjetoLoja.Entidades
{
    public class ItemPedido
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }

        public double Total => Produto.Preco * Quantidade;

        public ItemPedido(Produto produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
        }

        public override string ToString()
        {
            return $"{Produto.Nome} - {Quantidade} x R${Produto.Preco:F2} = {Total:F2}";
        }
    }
}
