using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoLoja.Models;
using ProjetoLoja.Service;
using ProjetoLoja.Interfaces;

namespace ProjetoLoja.Repositorios
{
    public class FornecedorRepositorioVetor : IFornecedorRepositorio
    {
        private Fornecedor[] fornecedores;
        private int proximoIndice;
        private int proximoId = 1;
        private const string CAMINHO_ARQUIVO = "fornecedores.json";
        private ArquivoUtil arquivoUtil;

        public FornecedorRepositorioVetor()
        {
            arquivoUtil = new ArquivoUtil();
            fornecedores = new Fornecedor[0]; // Começa com tamanho 0
            proximoIndice = 0;
            CarregarDados();
        }

        private void CarregarDados()
        {
            var lista = arquivoUtil.CarregarDeArquivo<Fornecedor>(CAMINHO_ARQUIVO);
            if (lista.Count > 0)
            {
                if (lista.Count > fornecedores.Length)
                {
                    RedimensionarVetor(lista.Count * 2);
                }

                for (int i = 0; i < lista.Count; i++)
                {
                    fornecedores[i] = lista[i];
                }
                proximoIndice = lista.Count;
                
                proximoId = lista.Max(f => f.Id) + 1;
            }
        }

        public void Salvar()
        {
            var lista = new List<Fornecedor>();
            for (int i = 0; i < proximoIndice; i++)
            {
                if (fornecedores[i] != null)
                {
                    lista.Add(fornecedores[i]);
                }
            }
            arquivoUtil.SalvarEmArquivo(CAMINHO_ARQUIVO, lista);
        }

        public void Adicionar(Fornecedor f)
        {
            RedimensionarVetor(proximoIndice + 1);

            f.Id = proximoId++;
            fornecedores[proximoIndice] = f;
            proximoIndice++;
            Salvar();
        }

        public bool Remover(int id)
        {
            for (int i = 0; i < proximoIndice; i++)
            {
                if (fornecedores[i] != null && fornecedores[i].Id == id)
                {
                    for (int j = i; j < proximoIndice - 1; j++)
                    {
                        fornecedores[j] = fornecedores[j + 1];
                    }
                    fornecedores[proximoIndice - 1] = null;
                    proximoIndice--;
                    Salvar();
                    return true;
                }
            }
            return false;
        }

        public Fornecedor Buscar(int id)
        {
            for (int i = 0; i < proximoIndice; i++)
            {
                if (fornecedores[i] != null && fornecedores[i].Id == id)
                {
                    return fornecedores[i];
                }
            }
            return null;
        }

        public List<Fornecedor> ListarTodos()
        {
            var lista = new List<Fornecedor>();
            for (int i = 0; i < proximoIndice; i++)
            {
                if (fornecedores[i] != null)
                {
                    lista.Add(fornecedores[i]);
                }
            }
            return lista;
        }

        public void Atualizar(Fornecedor f)
        {
            Salvar();
        }

        private void RedimensionarVetor(int novoTamanho)
        {
            var novoVetor = new Fornecedor[novoTamanho];
            for (int i = 0; i < proximoIndice; i++)
            {
                novoVetor[i] = fornecedores[i];
            }
            fornecedores = novoVetor;
        }
    }
}
