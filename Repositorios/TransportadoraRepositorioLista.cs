using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoLoja.Models;
using ProjetoLoja.Service;
using ProjetoLoja.Interfaces;

namespace ProjetoLoja.Repositorios
{
    public class TransportadoraRepositorioLista : ITransportadoraRepositorio
    {
        private List<Transportadora> transportadoras;
        private const string CAMINHO_ARQUIVO = "transportadoras.json";
        private int proximoId = 1;
        private ArquivoUtil arquivoUtil;

        public TransportadoraRepositorioLista()
        {
            arquivoUtil = new ArquivoUtil();
            transportadoras = arquivoUtil.CarregarDeArquivo<Transportadora>(CAMINHO_ARQUIVO);
            
            if (transportadoras.Count > 0)
            {
                proximoId = transportadoras.Max(t => t.Id) + 1;
            }
        }

        public void Salvar() => arquivoUtil.SalvarEmArquivo(CAMINHO_ARQUIVO, transportadoras);

        public void Adicionar(Transportadora t)
        {
            t.Id = proximoId++;
            transportadoras.Add(t);
            Salvar();
        }

        public bool Remover(int id)
        {
            bool removido = transportadoras.RemoveAll(t => t.Id == id) > 0;
            if (removido)
            {
                Salvar();
            }
            return removido;
        }

        public Transportadora Buscar(int id)
        {
            return transportadoras.FirstOrDefault(t => t.Id == id);
        }

        public List<Transportadora> ListarTodos()
        {
            return transportadoras.ToList();
        }

        public void Atualizar(Transportadora t)
        {
            Salvar();
        }
    }
}
