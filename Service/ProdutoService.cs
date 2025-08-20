using ProjetoLoja.Models;
using System;
using System.Reflection.Metadata.Ecma335;
using ProjetoLoja.Repositorios;
using ProjetoLoja.Interfaces;
using ProjetoLoja.Entidades.Exceptions;

namespace ProjetoLoja.Services
{
    public class ProdutoService
    {
        private IProdutoRepositorio repositorio;
        private IFornecedorRepositorio fornecedorRepositorio;

        public ProdutoService(IProdutoRepositorio repositorio, IFornecedorRepositorio fornecedorRepositorio)
        {
            this.repositorio = repositorio;
            this.fornecedorRepositorio = fornecedorRepositorio;
        }

        public void Menu()
        {
            int opcao;
            do
            {
                Console.WriteLine("\n==== Menu de Produtos ====");
                Console.WriteLine("1- Adicionar Produto");
                Console.WriteLine("2- Listar Produtos");
                Console.WriteLine("3- Editar Produto");
                Console.WriteLine("4- Remover Produto");
                Console.WriteLine("5- Buscar Produto por Código");
                Console.WriteLine("0- Voltar");
                Console.WriteLine("Opcão: ");
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
            Console.WriteLine("=== Adicionar Produto ===");
            
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Estoque: ");
            int estoque = int.Parse(Console.ReadLine());

            Console.Write("Preço: ");
            double preco = double.Parse(Console.ReadLine());

            var fornecedores = fornecedorRepositorio.ListarTodos();
            if (fornecedores.Count == 0)
            {
                Console.WriteLine("Nenhum fornecedor cadastrado. Cadastre um fornecedor antes de adicionar produtos.");
                return;
            }

            Console.WriteLine("Escolha o fornecedor pelo ID:");
            fornecedores.ForEach(f => Console.WriteLine($"ID: {f.Id} - Nome: {f.Nome}"));

            Console.Write("ID do fornecedor: ");
            int idFornecedor = int.Parse(Console.ReadLine());
            var fornecedor = fornecedores.FirstOrDefault(f => f.Id == idFornecedor);

            if (fornecedor == null)
            {
                Console.WriteLine("Fornecedor não encontrado!");
                return;
            }

            Produto p = new Produto(nome, descricao, estoque, preco, fornecedor);
            repositorio.Adicionar(p);

            Console.WriteLine($"Produto adicionado com sucesso! Código gerado: {p.Id}");
        }

        private void Listar()
        {
            Console.WriteLine("=== Lista de Produtos ===");
            var lista = repositorio.ListarTodos();
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
            }
            else
            {
                foreach (var produto in lista)
                {
                    Console.WriteLine(produto);
                }
            }
        }

        private void Remover()
        {
            Console.WriteLine("=== Remover Produto ===");
            Console.WriteLine("Informe o código do produto: ");
            int codigo = int.Parse(Console.ReadLine());

            if (repositorio.Remover(codigo))
            {
                Console.WriteLine("Produto removido com sucesso!");
            }
            else
            {
                Console.WriteLine("Produto não encontrado.");
            }
        }
        
        private void Buscar()
        {
            try
            {
                Console.WriteLine("=== Buscar Produto por Código ===");
                Console.Write("Código: ");
                if (int.TryParse(Console.ReadLine(), out int codigo))
                {
                    Produto produto = repositorio.Buscar(codigo);
                    if (produto != null)
                    {
                        Console.WriteLine(produto);
                    }
                    else
                    {
                        throw new ProdutoNaoEncontradoException(codigo);
                    }
                }
                else
                {
                    Console.WriteLine("Código inválido.");
                }
            }
            catch (ProdutoNaoEncontradoException ex)
            {
                Console.WriteLine($"[!] {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[!] Erro inesperado: {ex.Message}");
            }
        }

        private void Editar()
        {
            Console.WriteLine("=== Editar Produto ===");
            Console.Write("ID do produto a editar: ");
            int id = int.Parse(Console.ReadLine() ?? "0");
            
            var produto = repositorio.Buscar(id);
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado!");
                return;
            }

            Console.WriteLine($"Produto encontrado: {produto.Nome}");
            Console.WriteLine("Deixe em branco para manter o valor atual:");
            
            Console.Write($"Nome atual: {produto.Nome}. Novo nome: ");
            string novoNome = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoNome))
                produto.Nome = novoNome;

            Console.Write($"Descrição atual: {produto.Descricao}. Nova descrição: ");
            string novaDescricao = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novaDescricao))
                produto.Descricao = novaDescricao;

            Console.Write($"Estoque atual: {produto.Estoque}. Novo estoque: ");
            string novoEstoqueStr = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoEstoqueStr) && int.TryParse(novoEstoqueStr, out int novoEstoque))
                produto.Estoque = novoEstoque;

            Console.Write($"Preço atual: {produto.Preco:C}. Novo preço: ");
            string novoPrecoStr = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoPrecoStr) && double.TryParse(novoPrecoStr, out double novoPreco))
                produto.Preco = novoPreco;

            repositorio.Atualizar(produto);
            Console.WriteLine("Produto editado com sucesso!");
        }
    }
}
