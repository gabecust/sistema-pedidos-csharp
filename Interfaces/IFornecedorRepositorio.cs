using System;
using System.Collections.Generic;
using ProjetoLoja.Models;

namespace ProjetoLoja.Interfaces
{
    public interface IFornecedorRepositorio
    {
        void Adicionar(Fornecedor f);
        bool Remover(int id);
        Fornecedor Buscar(int id);
        List<Fornecedor> ListarTodos();
        void Atualizar(Fornecedor f);
    }
}
