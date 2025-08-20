using System;
using System.Collections.Generic;
using ProjetoLoja.Models;

namespace ProjetoLoja.Interfaces
{
    public interface ITransportadoraRepositorio
    {
        void Adicionar(Transportadora t);
        bool Remover(int id);
        Transportadora Buscar(int id);
        List<Transportadora> ListarTodos();
        void Atualizar(Transportadora t);
    }
}
