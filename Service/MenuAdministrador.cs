using System;
using ProjetoLoja.Repositorios;
using ProjetoLoja.Interfaces;
using ProjetoLoja.Entidades;
using ProjetoLoja.Services;

namespace ProjetoLoja.Service
{
    public class MenuAdministrador
    {
        private ProdutoService produtoService;
        private FornecedorService fornecedorService;
        private TransportadoraService transportadoraService;
        private ClienteService clienteService;
        private PedidoService pedidoService;

        public MenuAdministrador(
            IProdutoRepositorio produtoRepo,
            IFornecedorRepositorio fornecedorRepo,
            ITransportadoraRepositorio transportadoraRepo,
            IClienteRepositorio clienteRepo,
            PedidoService pedidoService)
        {
            produtoService = new ProdutoService(produtoRepo, fornecedorRepo);
            fornecedorService = new FornecedorService(fornecedorRepo);
            transportadoraService = new TransportadoraService(transportadoraRepo);
            clienteService = new ClienteService(clienteRepo);
            this.pedidoService = pedidoService;
        }

        public void Exibir()
        {
            int opcao;
            do
            {
                Console.WriteLine("\n=== Menu Administrador ===");
                Console.WriteLine("1- Gerenciar Produtos");
                Console.WriteLine("2- Gerenciar Fornecedores");
                Console.WriteLine("3- Gerenciar Transportadoras");
                Console.WriteLine("4- Gerenciar Clientes");
                Console.WriteLine("5- Gerenciar Pedido");
                Console.WriteLine("0- Sair");
                Console.Write("Opção: ");
                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1: produtoService.Menu(); break;
                    case 2: fornecedorService.Menu(); break;
                    case 3: transportadoraService.Menu(); break;
                    case 4: clienteService.Menu(); break;
                    case 5: pedidoService.Menu(); break;
                }
            } while (opcao != 0);
        }
    }
}
