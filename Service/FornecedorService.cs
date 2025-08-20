using ProjetoLoja.Models;
using ProjetoLoja.Repositorios;
using ProjetoLoja.Interfaces;
using System;

namespace ProjetoLoja.Services
{
    public class FornecedorService
    {
        private IFornecedorRepositorio repositorio;

        public FornecedorService(IFornecedorRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void Menu()
        {
            int opcao;
            do
            {
                Console.WriteLine("\n=== Menu Fornecedor ===");
                Console.WriteLine("1- Adicionar Fornecedor");
                Console.WriteLine("2- Listar Fornecedores");
                Console.WriteLine("3- Editar Fornecedor");
                Console.WriteLine("4- Remover Fornecedor");
                Console.WriteLine("5- Buscar Fornecedor por ID");
                Console.WriteLine("0- Voltar");
                Console.Write("Escolha uma opção: ");
                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1: Adicionar(); break;
                    case 2: Listar(); break;
                    case 3: Editar(); break;
                    case 4: Remover(); break;
                    case 5: BuscarPorId(); break;
                    case 0: Console.WriteLine("Voltando ao menu principal..."); break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            } while (opcao != 0);
        }

        private void Adicionar()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.WriteLine("=== Dados do Endereço ===");
            Console.Write("Rua: ");
            string rua = Console.ReadLine();

            Console.Write("Número: ");
            string numero = Console.ReadLine();

            Console.Write("Complemento: ");
            string complemento = Console.ReadLine();

            Console.Write("Bairro: ");
            string bairro = Console.ReadLine();

            Console.Write("CEP: ");
            string cep = Console.ReadLine();

            Console.Write("Cidade: ");
            string cidade = Console.ReadLine();

            Console.Write("Estado: ");
            string estado = Console.ReadLine();

            var endereco = new Endereco(rua, numero, complemento, bairro, cep, cidade, estado);
            var f = new Fornecedor(nome, descricao, telefone, email, endereco);
            repositorio.Adicionar(f);

            Console.WriteLine("Fornecedor adicionado com sucesso!");
            Console.WriteLine($"ID gerado: {f.Id}");
        }

        private void Listar()
        {
            Console.WriteLine("=== Lista de Fornecedores ===");
            var lista = repositorio.ListarTodos();
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum fornecedor cadastrado.");
            }
            else
            {
                foreach (var fornecedor in lista)
                {
                    Console.WriteLine(fornecedor);
                }
            }
        }

        private void Remover()
        {
            Console.WriteLine("Informe o ID do fornecedor a ser removido:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (repositorio.Remover(id))
                {
                    Console.WriteLine("Fornecedor removido com sucesso!");
                }
                else
                {
                    Console.WriteLine("Fornecedor não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        private void BuscarPorId()
        {
            Console.WriteLine("Informe o ID do fornecedor: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var fornecedor = repositorio.Buscar(id);
                if (fornecedor != null)
                {
                    Console.WriteLine($"ID: {fornecedor.Id}, Nome: {fornecedor.Nome}");
                    Console.WriteLine($"Descrição: {fornecedor.Descricao}");
                    Console.WriteLine($"Telefone: {fornecedor.Telefone}, Email: {fornecedor.Email}");
                    Console.WriteLine($"Endereço: {fornecedor.Endereco.Rua}, {fornecedor.Endereco.Numero}, {fornecedor.Endereco.Cidade}");
                }
                else
                {
                    Console.WriteLine("Fornecedor não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        private void Editar()
        {
            Console.WriteLine("=== Editar Fornecedor ===");
            Console.Write("ID do fornecedor a editar: ");
            int id = int.Parse(Console.ReadLine() ?? "0");
            
            var fornecedor = repositorio.Buscar(id);
            if (fornecedor == null)
            {
                Console.WriteLine("Fornecedor não encontrado!");
                return;
            }

            Console.WriteLine($"Fornecedor encontrado: {fornecedor.Nome}");
            Console.WriteLine("Deixe em branco para manter o valor atual:");
            
            Console.Write($"Nome atual: {fornecedor.Nome}. Novo nome: ");
            string novoNome = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoNome))
                fornecedor.Nome = novoNome;

            Console.Write($"Descrição atual: {fornecedor.Descricao}. Nova descrição: ");
            string novaDescricao = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novaDescricao))
                fornecedor.Descricao = novaDescricao;

            Console.Write($"Telefone atual: {fornecedor.Telefone}. Novo telefone: ");
            string novoTelefone = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoTelefone))
                fornecedor.Telefone = novoTelefone;

            Console.Write($"Email atual: {fornecedor.Email}. Novo email: ");
            string novoEmail = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoEmail))
                fornecedor.Email = novoEmail;

            repositorio.Atualizar(fornecedor);
            Console.WriteLine("Fornecedor editado com sucesso!");
        }
    }
}
