using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoLoja.Models;
using ProjetoLoja.Service;
using ProjetoLoja.Interfaces;

namespace ProjetoLoja.Repositorios
{
    public class FornecedorRepositorioLista : IFornecedorRepositorio
    {
        private List<Fornecedor> fornecedores;
        private const string CAMINHO_ARQUIVO = "fornecedores.json";
        private int proximoId = 1;
        private ArquivoUtil arquivoUtil;

        public FornecedorRepositorioLista()
        {
            arquivoUtil = new ArquivoUtil();
            fornecedores = arquivoUtil.CarregarDeArquivo<Fornecedor>(CAMINHO_ARQUIVO);
            
            if (fornecedores.Count > 0)
            {
                proximoId = fornecedores.Max(f => f.Id) + 1;
            }
        }

        public void Salvar() => arquivoUtil.SalvarEmArquivo(CAMINHO_ARQUIVO, fornecedores);

        public void Adicionar(Fornecedor f)
        {
            f.Id = proximoId++;
            fornecedores.Add(f);
            Salvar();
        }

        public bool Remover(int id)
        {
            bool removido = fornecedores.RemoveAll(f => f.Id == id) > 0;
            if (removido)
            {
                Salvar();
            }
            return removido;
        }

        public Fornecedor Buscar(int id)
        {
            return fornecedores.FirstOrDefault(f => f.Id == id);
        }

        public List<Fornecedor> ListarTodos()
        {
            return fornecedores.ToList(); // Retorna uma cópia da lista
        }

        public void Atualizar(Fornecedor f)
        {
            Salvar();
        }
    }
}
