using ProjetoLoja.Services;
using System;
using ProjetoLoja.Repositorios;
using ProjetoLoja.Service;
using ProjetoLoja.Entidades;
using ProjetoLoja.Interfaces;
using ProjetoLoja.Models;

public class Menu
{
    private ProdutoService produtoService;
    private FornecedorService fornecedorService;
    private TransportadoraService transportadoraService;
    private ClienteService clienteService;
    private PedidoService pedidoService;
    
    private IProdutoRepositorio produtoRepo;
    private IFornecedorRepositorio fornecedorRepo;
    private ITransportadoraRepositorio transportadoraRepo;
    private IClienteRepositorio clienteRepo;

    public Menu()
    {
        Console.WriteLine("=== Configuração do Sistema ===");
        Console.WriteLine("Escolha o tipo de repositório:");
        Console.WriteLine("1- Lista");
        Console.WriteLine("2- Vetor");
        Console.Write("Opção (1 ou 2): ");
        
        int opcaoRepo;
        while (!int.TryParse(Console.ReadLine(), out opcaoRepo) || (opcaoRepo != 1 && opcaoRepo != 2))
        {
            Console.Write("Opção inválida! Digite 1 para Lista ou 2 para Vetor: ");
        }
        
        if (opcaoRepo == 2) // Vetor
        {
            Console.WriteLine("Repositório vetor selecionado!");
            produtoRepo = new ProdutoRepositorioVetor();
            fornecedorRepo = new FornecedorRepositorioVetor();
            transportadoraRepo = new TransportadoraRepositorioVetor();
            clienteRepo = new ClienteRepositorioVetor();
            
            produtoService = new ProdutoService(produtoRepo, fornecedorRepo);
            fornecedorService = new FornecedorService(fornecedorRepo);
            transportadoraService = new TransportadoraService(transportadoraRepo);
            clienteService = new ClienteService(clienteRepo);
        }
        else // Lista
        {
            Console.WriteLine("Repositório lista selecionado!");
            produtoRepo = new ProdutoRepositorioLista();
            fornecedorRepo = new FornecedorRepositorioLista();
            transportadoraRepo = new TransportadoraRepositorioLista();
            clienteRepo = new ClienteRepositorioLista();
            
            produtoService = new ProdutoService(produtoRepo, fornecedorRepo);
            fornecedorService = new FornecedorService(fornecedorRepo);
            transportadoraService = new TransportadoraService(transportadoraRepo);
            clienteService = new ClienteService(clienteRepo);
        }
        
        pedidoService = new PedidoService();
    }

    public void IniciarSistema()
    {
        var usuarioService = new UsuarioService(clienteRepo);
        Usuario usuario = usuarioService.Login();

        if (usuario == null)
        {
            Console.WriteLine("Encerrando aplicação...");
            return;
        }

        if (usuario is Administrador)
        {
            var menuAdmin = new MenuAdministrador(
                produtoRepo, 
                fornecedorRepo, 
                transportadoraRepo, 
                clienteRepo, 
                pedidoService
            );
            menuAdmin.Exibir();
        }
        else if (usuario is Cliente cliente)
        {
            var carrinhoService = new CarrinhoService(
                produtoRepo, 
                transportadoraRepo
            );
            var menuCliente = new MenuCliente(cliente, carrinhoService);
            menuCliente.Exibir();
        }

        Console.WriteLine("Pressione ENTER para sair...");
        Console.ReadLine();
    }

    public void ExibirMenu()
    {
        int opcao;
        do
        {
            Console.WriteLine("\n=== Menu Principal ===");
            Console.WriteLine("1- Gerenciar Produtos");
            Console.WriteLine("2- Gerenciar Fornecedores");
            Console.WriteLine("3- Gerenciar Transportadoras");
            Console.WriteLine("4- Gerenciar Clientes");
            Console.WriteLine("5- Gerenciar Pedidos");
            Console.WriteLine("0- Sair");
            Console.WriteLine("Opção: ");
            int.TryParse(Console.ReadLine(), out opcao);

            switch (opcao)
            {
                case 1: produtoService.Menu(); break;
                case 2: fornecedorService.Menu(); break;
                case 3: transportadoraService.Menu(); break;
                case 4: clienteService.Menu(); break;
                case 5: pedidoService.Menu(); break;

                case 0: Console.WriteLine("Saindo do sistema..."); break;
            }
        } while (opcao != 0);
    }
}
