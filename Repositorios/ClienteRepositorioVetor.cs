using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoLoja.Models;
using ProjetoLoja.Service;
using ProjetoLoja.Interfaces;

namespace ProjetoLoja.Repositorios
{
    public class ClienteRepositorioVetor : IClienteRepositorio
    {
        private Cliente[] clientes;
        private int proximoIndice;
        private int proximoId = 1;
        private const string CAMINHO_ARQUIVO = "clientes.json";
        private ArquivoUtil arquivoUtil;

        public ClienteRepositorioVetor()
        {
            arquivoUtil = new ArquivoUtil();
            clientes = new Cliente[0]; // Começa com tamanho 0
            proximoIndice = 0;
            CarregarDados();
        }

        private void CarregarDados()
        {
            var lista = arquivoUtil.CarregarDeArquivo<Cliente>(CAMINHO_ARQUIVO);
            if (lista.Count > 0)
            {
                if (lista.Count > clientes.Length)
                {
                    RedimensionarVetor(lista.Count * 2);
                }

                for (int i = 0; i < lista.Count; i++)
                {
                    clientes[i] = lista[i];
                }
                proximoIndice = lista.Count;
                proximoId = lista.Max(c => c.Id) + 1;
            }
        }

        public void Salvar()
        {
            var lista = new List<Cliente>();
            for (int i = 0; i < proximoIndice; i++)
            {
                if (clientes[i] != null)
                {
                    lista.Add(clientes[i]);
                }
            }
            arquivoUtil.SalvarEmArquivo(CAMINHO_ARQUIVO, lista);
        }

        public void Adicionar(Cliente c)
        {
            RedimensionarVetor(proximoIndice + 1);

            c.Id = proximoId++;
            clientes[proximoIndice] = c;
            proximoIndice++;
            Salvar();
        }

        public bool Remover(int id)
        {
            for (int i = 0; i < proximoIndice; i++)
            {
                if (clientes[i] != null && clientes[i].Id == id)
                {
                    for (int j = i; j < proximoIndice - 1; j++)
                    {
                        clientes[j] = clientes[j + 1];
                    }
                    clientes[proximoIndice - 1] = null;
                    proximoIndice--;
                    Salvar();
                    return true;
                }
            }
            return false;
        }

        public Cliente Buscar(int id)
        {
            for (int i = 0; i < proximoIndice; i++)
            {
                if (clientes[i] != null && clientes[i].Id == id)
                {
                    return clientes[i];
                }
            }
            return null;
        }

        public List<Cliente> ListarTodos()
        {
            var lista = new List<Cliente>();
            for (int i = 0; i < proximoIndice; i++)
            {
                if (clientes[i] != null)
                {
                    lista.Add(clientes[i]);
                }
            }
            return lista;
        }

        public void Atualizar(Cliente c)
        {
            Salvar();
        }

        private void RedimensionarVetor(int novoTamanho)
        {
            var novoVetor = new Cliente[novoTamanho];
            for (int i = 0; i < proximoIndice; i++)
            {
                novoVetor[i] = clientes[i];
            }
            clientes = novoVetor;
        }
    }
}
