using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoLoja.Models;
using ProjetoLoja.Service;
using ProjetoLoja.Interfaces;

namespace ProjetoLoja.Repositorios
{
    public class TransportadoraRepositorioVetor : ITransportadoraRepositorio
    {
        private Transportadora[] transportadoras;
        private int proximoIndice;
        private int proximoId = 1;
        private const string CAMINHO_ARQUIVO = "transportadoras.json";
        private ArquivoUtil arquivoUtil;

        public TransportadoraRepositorioVetor()
        {
            arquivoUtil = new ArquivoUtil();
            transportadoras = new Transportadora[0];
            proximoIndice = 0;
            CarregarDados();
        }

        private void CarregarDados()
        {
            var lista = arquivoUtil.CarregarDeArquivo<Transportadora>(CAMINHO_ARQUIVO);
            if (lista.Count > 0)
            {
                if (lista.Count > transportadoras.Length)
                {
                    RedimensionarVetor(lista.Count * 2);
                }

                for (int i = 0; i < lista.Count; i++)
                {
                    transportadoras[i] = lista[i];
                }
                proximoIndice = lista.Count;
                proximoId = lista.Max(t => t.Id) + 1;
            }
        }

        public void Salvar()
        {
            var lista = new List<Transportadora>();
            for (int i = 0; i < proximoIndice; i++)
            {
                if (transportadoras[i] != null)
                {
                    lista.Add(transportadoras[i]);
                }
            }
            arquivoUtil.SalvarEmArquivo(CAMINHO_ARQUIVO, lista);
        }

        public void Adicionar(Transportadora t)
        {
            RedimensionarVetor(proximoIndice + 1);

            t.Id = proximoId++;
            transportadoras[proximoIndice] = t;
            proximoIndice++;
            Salvar();
        }

        public bool Remover(int id)
        {
            for (int i = 0; i < proximoIndice; i++)
            {
                if (transportadoras[i] != null && transportadoras[i].Id == id)
                {
                    for (int j = i; j < proximoIndice - 1; j++)
                    {
                        transportadoras[j] = transportadoras[j + 1];
                    }
                    transportadoras[proximoIndice - 1] = null;
                    proximoIndice--;
                    Salvar();
                    return true;
                }
            }
            return false;
        }

        public Transportadora Buscar(int id)
        {
            for (int i = 0; i < proximoIndice; i++)
            {
                if (transportadoras[i] != null && transportadoras[i].Id == id)
                {
                    return transportadoras[i];
                }
            }
            return null;
        }

        public List<Transportadora> ListarTodos()
        {
            var lista = new List<Transportadora>();
            for (int i = 0; i < proximoIndice; i++)
            {
                if (transportadoras[i] != null)
                {
                    lista.Add(transportadoras[i]);
                }
            }
            return lista;
        }

        public void Atualizar(Transportadora t)
        {
            Salvar();
        }

        private void RedimensionarVetor(int novoTamanho)
        {
            var novoVetor = new Transportadora[novoTamanho];
            for (int i = 0; i < proximoIndice; i++)
            {
                novoVetor[i] = transportadoras[i];
            }
            transportadoras = novoVetor;
        }
    }
}
