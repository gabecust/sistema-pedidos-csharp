using System;
using System.Collections.Generic;
using ProjetoLoja.Models;

namespace ProjetoLoja.Interfaces
{
    public interface IProdutoRepositorio
    {
        void Adicionar(Produto p);
        bool Remover(int id);
        Produto Buscar(int id);
        List<Produto> ListarTodos();
        void Atualizar(Produto p);
    }
}
