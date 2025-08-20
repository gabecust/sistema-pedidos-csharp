using System;
using System.Collections.Generic;
using ProjetoLoja.Models;

namespace ProjetoLoja.Interfaces
{
    public interface IClienteRepositorio
    {
        void Adicionar(Cliente c);
        bool Remover(int id);
        Cliente Buscar(int id);
        List<Cliente> ListarTodos();
        void Atualizar(Cliente c);
    }
}
