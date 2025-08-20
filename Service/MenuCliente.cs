using System;
using ProjetoLoja.Models;
using ProjetoLoja.Entidades;
using ProjetoLoja.Repositorios;

namespace ProjetoLoja.Service
{
    public class MenuCliente
    {
        private Cliente cliente;
        private CarrinhoService carrinhoService;

        public MenuCliente(Cliente cliente, CarrinhoService carrinhoService)
        {
            this.cliente = cliente;
            this.carrinhoService = carrinhoService;
        }

        public void Exibir()
        {
            int opcao;
            do
            {
                Console.WriteLine("\n=== Menu Cliente ===");
                Console.WriteLine($"Olá, {cliente.NomeCompleto}! Seja bem-vindo");
                Console.WriteLine("1- Fazer pedido");
                Console.WriteLine("2- Meus pedidos");
                Console.WriteLine("3- Buscar pedidos por Número");
                Console.WriteLine("4- Buscar pedidos por Data");
                Console.WriteLine("0- Sair");
                Console.Write("Opção: ");
                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1: carrinhoService.Comprar(cliente); break;
                    case 2: carrinhoService.ConsultarPedidos(cliente); break;
                    case 3:
                        Console.WriteLine("Número do pedido: ");
                        int numero = int.Parse(Console.ReadLine());
                        carrinhoService.ConsultarPorNumero(numero, cliente);
                        break;
                    case 4:
                        Console.WriteLine("Data inicial (dd/mm/aaaa): ");
                        DateTime inicio = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Data final (dd/mm/aaaa): ");
                        DateTime fim = DateTime.Parse(Console.ReadLine());
                        carrinhoService.ConsultarPorIntervalo(inicio, fim, cliente);
                        break;
                }
            } while (opcao != 0);
        }
    }
}
