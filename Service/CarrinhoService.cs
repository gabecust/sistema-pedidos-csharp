using System;
using System.Collections.Generic;
using ProjetoLoja.Entidades;
using ProjetoLoja.Repositorios;
using ProjetoLoja.Interfaces;
using System.Linq;
using ProjetoLoja.Models;
using ProjetoLoja.Entidades.Exceptions;
using ProjetoLoja.Services;

namespace ProjetoLoja.Service
{
    public class CarrinhoService
    {
        private int proximoNumero = 1;
        private IProdutoRepositorio produtoRepo;
        private ITransportadoraRepositorio transportadoraRepo;
        private List<Pedido> pedidos = new List<Pedido>();
        private const string CAMINHO_ARQUIVO = "pedidos.json";

        public CarrinhoService(IProdutoRepositorio produtoRepo, ITransportadoraRepositorio transportadoraRepo)
        {
            this.produtoRepo = produtoRepo;
            this.transportadoraRepo = transportadoraRepo;
            var arquivoUtil = new ArquivoUtil();
            pedidos = arquivoUtil.CarregarDeArquivo<Pedido>("pedidos.json");
            if (pedidos.Any())
            {
                proximoNumero = pedidos.Max(p => p.Numero) + 1;
            }

        }



        public void Comprar(Cliente cliente)
        {
            ArquivoUtil arquivoUtil = new ArquivoUtil();
            try
            {
                var itens = new List<ItemPedido>();
                while (true)
                {
                    Console.WriteLine("\nProdutos disponíveis:");
                    var produtos = produtoRepo.ListarTodos().Where(p => p.Estoque > 0).ToList();

                    for (int i = 0; i < produtos.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {produtos[i]}");
                    }

                    Console.Write("Escolha o produto desejado (número) ou 0 para finalizar: ");
                    if (!int.TryParse(Console.ReadLine(), out int escolha) || escolha == 0) break;

                    if (escolha < 1 || escolha > produtos.Count)
                    {
                        throw new ProdutoNaoEncontradoException("Produto não encontrado.");
                    }

                    var produto = produtos[escolha - 1];

                    Console.Write($"Estoque: {produto.Estoque} - Quantidade desejada: ");
                    if (int.TryParse(Console.ReadLine(), out int qtd))
                    {
                        if (qtd <= 0)
                        {
                            Console.WriteLine("Quantidade deve ser maior que zero.");
                            continue;
                        }

                        if (qtd > produto.Estoque)
                        {
                            throw new EstoqueInsuficienteException(produto.Nome, qtd, produto.Estoque);
                        }

                        produto.Estoque -= qtd;
                        produtoRepo.Atualizar(produto);
                        itens.Add(new ItemPedido(produto, qtd));

                        Console.WriteLine($"{produto.Nome} adicionado ao carrinho!");
                    }
                    else
                    {
                        Console.WriteLine("Quantidade inválida.");
                    }
                }

                if (itens.Count == 0)
                {
                    Console.WriteLine("Carrinho vazio. Cancelando a compra...");
                    return;
                }

                Console.WriteLine("\nTransportadoras disponíveis: ");
                var listaT = transportadoraRepo.ListarTodos();
                for (int i = 0; i < listaT.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {listaT[i]}");
                }

                Console.Write("Escolha uma transportadora: ");
                if (int.TryParse(Console.ReadLine(), out int escolhaT) && escolhaT > 0 && escolhaT <= listaT.Count)
                {
                    var transportadora = listaT[escolhaT - 1];

                    var pedido = new Pedido(proximoNumero, cliente, itens, transportadora);
                    proximoNumero++;
                    pedidos.Add(pedido);

                    Console.WriteLine("\nPedido realizado com sucesso!");
                    Console.WriteLine(pedido);
                    arquivoUtil.SalvarEmArquivo(CAMINHO_ARQUIVO, pedidos);
                }
                else
                {
                    Console.WriteLine("Escolha inválida de transportadora.");
                }

            }
            catch (EstoqueInsuficienteException ex)
            {
                Console.WriteLine($"[!] Erro de Estoque: {ex.Message}");
                if (ex.EstoqueDisponivel > 0)
                {
                    Console.WriteLine($"[!] Sugestão: Máximo disponível é {ex.EstoqueDisponivel} unidades.");
                }
                else
                {
                    Console.WriteLine($"[!] Produto '{ex.NomeProduto}' está indisponível (estoque zerado).");
                }
            }
            catch (ProdutoNaoEncontradoException ex)
            {
                Console.WriteLine($"[!] Erro: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("[!] Erro: Valor inválido digitado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[!] Erro inesperado: {ex.Message}");
            }

            arquivoUtil.SalvarEmArquivo(CAMINHO_ARQUIVO, pedidos);
        }


        public void ConsultarPedidos(Cliente cliente)
        {
            var meusPedidos = pedidos.Where(p => p.Cliente.NomeUsuario == cliente.NomeUsuario).ToList();

            if (meusPedidos.Count == 0)
            {
                Console.WriteLine("Você ainda não possui pedidos.");
                return;
            }

            foreach (var pedido in meusPedidos)
            {
                Console.WriteLine("\n---PEDIDO---");
                Console.WriteLine(pedido);
            }
        }

        public void ConsultarPorNumero(int numero, Cliente cliente)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Numero == numero && p.Cliente.NomeUsuario == cliente.NomeUsuario);
            if (pedido != null)
            {
                Console.WriteLine("\n--- Detalhes do Pedido ---");
                Console.WriteLine(pedido);
            }
            else
            {
                Console.WriteLine("Pedido não encontrado.");
            }
        }

        public void ConsultarPorIntervalo(DateTime inicio, DateTime fim, Cliente cliente)
        {
            var encontrados = pedidos
                .Where(p => p.Cliente.NomeUsuario == cliente.NomeUsuario && p.Data <= fim).ToList();

            if (encontrados.Count == 0)
            {
                Console.WriteLine("Nenhum pedido encontrado no intervalo infomado.");
                return;
            }

            foreach (var pedido in encontrados)
            {
                Console.WriteLine("\n---PEDIDO---");
                Console.WriteLine(pedido);
            }
                
        }
    }
}
