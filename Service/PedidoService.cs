using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoLoja.Entidades;
using ProjetoLoja.Entidades.Enums;

namespace ProjetoLoja.Service
{
    public class PedidoService
    {
        private List<Pedido> pedidos;
        private const string CAMINHO_ARQUIVO = "pedidos.json";
        private ArquivoUtil arquivoUtil;

        public PedidoService()
        {
            this.arquivoUtil = new ArquivoUtil();
            this.pedidos = arquivoUtil.CarregarDeArquivo<Pedido>(CAMINHO_ARQUIVO);
        }

        public void Menu()
        {
            int opcao;
            do
            {
                Console.WriteLine("\n=== Gerenciar Pedidos ===");
                Console.WriteLine("1- Listar Todos os Pedidos");
                Console.WriteLine("2- Consultar Pedido por Número");
                Console.WriteLine("3- Alterar Status do Pedido");
                Console.WriteLine("0- Voltar");
                Console.Write("Opcao: ");
                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1: ListarTodos(); break;
                    case 2: ConsultarPorNumero(); break;
                    case 3: AlterarStatus(); break;
                }

            } while (opcao != 0);
        }

        private void ListarTodos()
        {
            if (pedidos.Count == 0)
            {
                Console.WriteLine("Nenhum pedido encontrado");
                return;
            }

            foreach (var pedido in pedidos)
            {
                Console.WriteLine("\n--- PEDIDO --- ");
                Console.WriteLine(pedido);
            }
        }

        private void ConsultarPorNumero()
        {
            Console.Write("Número do Pedido: ");
            if (int.TryParse(Console.ReadLine(), out int numero))
            {
                var pedido = pedidos.FirstOrDefault(p => p.Numero == numero);
                if (pedido != null)
                {
                    Console.WriteLine(pedido);
                }
                else
                {
                    Console.WriteLine("Pedido não encontrado");
                }
            }
            else
            {
                Console.WriteLine("Número inválido");
            }
        }

        private void AlterarStatus()
        {
            Console.Write("Número do Pedido: ");
            if (!int.TryParse(Console.ReadLine(), out int numero))
            {
                Console.WriteLine("Número inválido");
                return;
            }

            var pedido = pedidos.FirstOrDefault(p => p.Numero == numero);
            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado");
                return;
            }

            Console.WriteLine("Status atual: " + pedido.Status);
            
            switch (pedido.Status)
            {
                case StatusPedido.Novo:
                    Console.WriteLine("1- Marcar como Enviado");
                    Console.WriteLine("2- Cancelar Pedido");
                    Console.Write("Opcao: ");
                    int.TryParse(Console.ReadLine(), out int escolhaNovo);
                    
                    if (escolhaNovo == 1)
                    {
                        pedido.MarcarComoEnviado();
                        arquivoUtil.SalvarEmArquivo(CAMINHO_ARQUIVO, pedidos);
                        Console.WriteLine("Pedido marcado como Enviado.");
                    }
                    else if (escolhaNovo == 2)
                    {
                        pedido.Cancelar();
                        arquivoUtil.SalvarEmArquivo(CAMINHO_ARQUIVO, pedidos);
                        Console.WriteLine("Pedido cancelado.");
                    }
                    break;
                    
                case StatusPedido.Enviado:
                    Console.WriteLine("1- Marcar como Entregue");
                    Console.WriteLine("2- Cancelar Pedido");
                    Console.Write("Opcao: ");
                    int.TryParse(Console.ReadLine(), out int escolhaEnviado);
                    
                    if (escolhaEnviado == 1)
                    {
                        pedido.MarcarComoEntregue();
                        arquivoUtil.SalvarEmArquivo(CAMINHO_ARQUIVO, pedidos);
                        Console.WriteLine("Pedido marcado como Entregue.");
                    }
                    else if (escolhaEnviado == 2)
                    {
                        pedido.Cancelar();
                        arquivoUtil.SalvarEmArquivo(CAMINHO_ARQUIVO, pedidos);
                        Console.WriteLine("Pedido cancelado.");
                    }
                    break;
                    
                case StatusPedido.Entregue:
                    Console.WriteLine("Pedido já foi entregue. Não é possível alterar o status.");
                    break;
                    
                case StatusPedido.Cancelado:
                    Console.WriteLine("Pedido já foi cancelado. Não é possível alterar o status.");
                    break;
            }
        }

        public void AdicionarPedido(Pedido pedido)
        {
            pedidos.Add(pedido);
            arquivoUtil.SalvarEmArquivo(CAMINHO_ARQUIVO, pedidos);
        }

        public int ObterProximoNumero()
        {
            if (pedidos.Count == 0)
                return 1;
            return pedidos.Max(p => p.Numero) + 1;
        }
    }
}
