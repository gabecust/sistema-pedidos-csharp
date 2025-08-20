using ProjetoLoja.Models;
using ProjetoLoja.Repositorios;
using ProjetoLoja.Interfaces;
using System;
using System.Reflection;

namespace ProjetoLoja.Services
{
    public class ClienteService
    {
        private IClienteRepositorio repositorio;
        private EnderecoService enderecoService;

        public ClienteService(IClienteRepositorio repositorio)
        {
            this.repositorio = repositorio;
            this.enderecoService = new EnderecoService();
        }

        public void Menu()
        {
            int opcao;
            do
            {
                Console.WriteLine("\n=== Menu Clientes ===");
                Console.WriteLine("1- Adicionar Cliente");
                Console.WriteLine("2- Listar Clientes");
                Console.WriteLine("3- Editar Cliente");
                Console.WriteLine("4- Remover Cliente");
                Console.WriteLine("5- Buscar Cliente por Usuário");
                Console.WriteLine("0- Voltar");
                Console.Write("Opção: ");
                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1: Adicionar(); break;
                    case 2: Listar(); break;
                    case 3: Editar(); break;
                    case 4: Remover(); break;
                    case 5: Buscar(); break;
                }
            } while (opcao != 0);
        }

        private void Adicionar()
        {
            Console.WriteLine("Usuário: ");
            string usuario = Console.ReadLine();

            Console.WriteLine("Senha: ");
            string senha = Console.ReadLine();

            Console.WriteLine("Nome Completo: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Endereço: ");
            var endereco = enderecoService.LerEndereco();

            var cliente = new Cliente(usuario, senha, nome, endereco);
            repositorio.Adicionar(cliente);

            Console.WriteLine("Cliente adicionado com sucesso!");
        }

        private void Listar()
        {
            var lista = repositorio.ListarTodos();
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
            }
            else
            {
                Console.WriteLine("=== Lista de Clientes ===");
                foreach (var cliente in lista)
                {
                    Console.WriteLine(cliente);
                }
            }
        }

        private void Remover()
        {
            Console.WriteLine("Informe o ID do cliente: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (repositorio.Remover(id))
                {
                    Console.WriteLine("Cliente removido com sucesso!");
                }
                else
                {
                    Console.WriteLine("Cliente não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        private void Buscar()
        {
            Console.Write("Informe o ID do cliente: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var c = repositorio.Buscar(id);
                if (c != null)
                {
                    Console.WriteLine(c);
                }
                else
                {
                    Console.WriteLine("Cliente não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        private void Editar()
        {
            Console.WriteLine("=== Editar Cliente ===");
            Console.Write("ID do cliente a editar: ");
            int id = int.Parse(Console.ReadLine() ?? "0");
            
            var cliente = repositorio.Buscar(id);
            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado!");
                return;
            }

            Console.WriteLine($"Cliente encontrado: {cliente.NomeCompleto}");
            Console.WriteLine("Deixe em branco para manter o valor atual:");
            
            Console.Write($"Nome completo atual: {cliente.NomeCompleto}. Novo nome: ");
            string novoNome = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoNome))
                cliente.NomeCompleto = novoNome;

            Console.Write($"Usuário atual: {cliente.NomeUsuario}. Novo usuário: ");
            string novoUsuario = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoUsuario))
                cliente.NomeUsuario = novoUsuario;

            Console.Write("Nova senha (deixe em branco para manter): ");
            string novaSenha = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novaSenha))
                cliente.Senha = novaSenha;

            repositorio.Atualizar(cliente);
            Console.WriteLine("Cliente editado com sucesso!");
        }
    }
}
