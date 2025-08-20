using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoLoja.Models;
using ProjetoLoja.Service;
using ProjetoLoja.Interfaces;

namespace ProjetoLoja.Repositorios
{
    public class ClienteRepositorioLista : IClienteRepositorio
    {
        private List<Cliente> clientes;
        private const string CAMINHO_ARQUIVO = "clientes.json";
        private int proximoId = 1;
        private ArquivoUtil arquivoUtil;

        public ClienteRepositorioLista()
        {
            arquivoUtil = new ArquivoUtil();
            clientes = arquivoUtil.CarregarDeArquivo<Cliente>(CAMINHO_ARQUIVO);
            
            if (clientes.Count > 0)
            {
                proximoId = clientes.Max(c => c.Id) + 1;
            }
        }

        public void Salvar() => arquivoUtil.SalvarEmArquivo(CAMINHO_ARQUIVO, clientes);

        public void Adicionar(Cliente c)
        {
            c.Id = proximoId++;
            clientes.Add(c);
            Salvar();
        }

        public bool Remover(int id)
        {
            bool removido = clientes.RemoveAll(c => c.Id == id) > 0;
            if (removido)
            {
                Salvar();
            }
            return removido;
        }

        public Cliente Buscar(int id)
        {
            return clientes.FirstOrDefault(c => c.Id == id);
        }

        public List<Cliente> ListarTodos()
        {
            return clientes.ToList();
        }

        public void Atualizar(Cliente c)
        {
            Salvar();
        }
    }
}
