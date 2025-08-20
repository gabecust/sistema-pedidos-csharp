using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoLoja.Models;
using ProjetoLoja.Service;
using ProjetoLoja.Interfaces;

namespace ProjetoLoja.Repositorios
{
    public class ProdutoRepositorioVetor : IProdutoRepositorio
    {
        private Produto[] produtos;
        private int proximoIndice;
        private int proximoId = 1;
        private const string CAMINHO_ARQUIVO = "produtos.json";
        private ArquivoUtil arquivoUtil;

        public ProdutoRepositorioVetor()
        {
            arquivoUtil = new ArquivoUtil();
            produtos = new Produto[0]; // Começa com tamanho 0
            proximoIndice = 0;
            CarregarDados();
        }

        private void CarregarDados()
        {
            var lista = arquivoUtil.CarregarDeArquivo<Produto>(CAMINHO_ARQUIVO);
            if (lista.Count > 0)
            {
                if (lista.Count > produtos.Length)
                {
                    RedimensionarVetor(lista.Count * 2);
                }

                for (int i = 0; i < lista.Count; i++)
                {
                    produtos[i] = lista[i];
                }
                proximoIndice = lista.Count;
                proximoId = lista.Max(p => p.Id) + 1;
            }
        }

        public void Salvar()
        {
            var lista = new List<Produto>();
            for (int i = 0; i < proximoIndice; i++)
            {
                if (produtos[i] != null)
                {
                    lista.Add(produtos[i]);
                }
            }
            arquivoUtil.SalvarEmArquivo(CAMINHO_ARQUIVO, lista);
        }

        public void Adicionar(Produto p)
        {
            RedimensionarVetor(proximoIndice + 1);

            p.Id = proximoId++;
            produtos[proximoIndice] = p;
            proximoIndice++;
            Salvar();
        }

        public bool Remover(int id)
        {
            for (int i = 0; i < proximoIndice; i++)
            {
                if (produtos[i] != null && produtos[i].Id == id)
                {
                    for (int j = i; j < proximoIndice - 1; j++)
                    {
                        produtos[j] = produtos[j + 1];
                    }
                    produtos[proximoIndice - 1] = null;
                    proximoIndice--;
                    Salvar();
                    return true;
                }
            }
            return false;
        }

        public Produto Buscar(int id)
        {
            for (int i = 0; i < proximoIndice; i++)
            {
                if (produtos[i] != null && produtos[i].Id == id)
                {
                    return produtos[i];
                }
            }
            return null;
        }

        public List<Produto> ListarTodos()
        {
            var lista = new List<Produto>();
            for (int i = 0; i < proximoIndice; i++)
            {
                if (produtos[i] != null)
                {
                    lista.Add(produtos[i]);
                }
            }
            return lista;
        }

        public void Atualizar(Produto p)
        {
            Salvar();
        }

        private void RedimensionarVetor(int novoTamanho)
        {
            var novoVetor = new Produto[novoTamanho];
            for (int i = 0; i < proximoIndice; i++)
            {
                novoVetor[i] = produtos[i];
            }
            produtos = novoVetor;
        }
    }
}
