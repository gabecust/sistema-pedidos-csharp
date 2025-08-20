using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoLoja.Models;
using ProjetoLoja.Service;
using ProjetoLoja.Interfaces;

namespace ProjetoLoja.Repositorios
{
    public class ProdutoRepositorioLista : IProdutoRepositorio
    {
        private List<Produto> produtos;
        private const string CAMINHO_ARQUIVO = "produtos.json";
        private int proximoId = 1;
        private ArquivoUtil arquivoUtil;

        public ProdutoRepositorioLista()
        {
            arquivoUtil = new ArquivoUtil();
            produtos = arquivoUtil.CarregarDeArquivo<Produto>(CAMINHO_ARQUIVO);
            
            if (produtos.Count > 0)
            {
                proximoId = produtos.Max(p => p.Id) + 1;
            }
        }

        public void Salvar() => arquivoUtil.SalvarEmArquivo(CAMINHO_ARQUIVO, produtos);

        public void Adicionar(Produto p)
        {
            p.Id = proximoId++;
            produtos.Add(p);
            Salvar();
        }

        public bool Remover(int id)
        {
            bool removido = produtos.RemoveAll(p => p.Id == id) > 0;
            if (removido)
            {
                Salvar();
            }
            return removido;
        }

        public Produto Buscar(int id)
        {
            return produtos.FirstOrDefault(p => p.Id == id);
        }

        public List<Produto> ListarTodos()
        {
            return produtos.ToList();
        }

        public void Atualizar(Produto p)
        {
            Salvar();
        }
    }
}
