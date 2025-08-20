using ProjetoLoja.Models;
using ProjetoLoja.Repositorios;
using ProjetoLoja.Interfaces;
using System;

namespace ProjetoLoja.Services
{

    public class TransportadoraService
    {
        private ITransportadoraRepositorio repositorio;

        public TransportadoraService(ITransportadoraRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void Menu()
        {
            int opcao;
            do
            {
                Console.WriteLine("\n=== Menu Tranportadora ===");
                Console.WriteLine("1- Adicionar Transportadora");
                Console.WriteLine("2- Listar Transportadoras");
                Console.WriteLine("3- Editar Transportadora");
                Console.WriteLine("4- Remover Transportadora");
                Console.WriteLine("5- Buscar Transportadora");
                Console.WriteLine("0- Voltar");
                Console.WriteLine("Opção: ");
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
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine() ?? "";

            Console.WriteLine("Telefone: ");
            string telefone = Console.ReadLine() ?? "";

            Console.WriteLine("Valor Frete: ");
            double valorFrete = double.TryParse(Console.ReadLine(), out double valor) ? valor : 0.0;

            var t = new Transportadora(nome, telefone, valorFrete);
            repositorio.Adicionar(t);

            Console.WriteLine("Transportadora cadastrada com sucesso!");
            Console.WriteLine($"ID gerado: {t.Id}");
        }

        private void Listar()
        {
            Console.WriteLine("=== Lista de Transportadoras ===");
            var lista = repositorio.ListarTodos();
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma transportadora cadastrada.");
            }
            else
            {
                foreach (var transportadora in lista)
                {
                    Console.WriteLine(transportadora);
                }
            }
        }

        private void Remover()
        {
            Console.WriteLine("Informe o ID da tranportadora que será removida: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (repositorio.Remover(id))
                {
                    Console.WriteLine("Transportadora removida com sucesso!");
                }
                else
                {
                    Console.WriteLine("Transportadora não encontrada.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        private void Buscar()
        {
            Console.WriteLine("Informe o ID da tranportadora: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var t = repositorio.Buscar(id);
                if (t != null)
                {
                    Console.WriteLine($"ID: {t.Id}, Nome: {t.Nome}");
                    Console.WriteLine($"Telefone: {t.Telefone}, Valor Frete: {t.ValorFrete:C}");
                }
                else
                {
                    Console.WriteLine("Transportadora não encontrada.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        private void Editar()
        {
            Console.WriteLine("=== Editar Transportadora ===");
            Console.Write("ID da transportadora a editar: ");
            int id = int.Parse(Console.ReadLine() ?? "0");
            
            var transportadora = repositorio.Buscar(id);
            if (transportadora == null)
            {
                Console.WriteLine("Transportadora não encontrada!");
                return;
            }

            Console.WriteLine($"Transportadora encontrada: {transportadora.Nome}");
            Console.WriteLine("Deixe em branco para manter o valor atual:");
            
            Console.Write($"Nome atual: {transportadora.Nome}. Novo nome: ");
            string novoNome = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoNome))
                transportadora.Nome = novoNome;

            Console.Write($"Telefone atual: {transportadora.Telefone}. Novo telefone: ");
            string novoTelefone = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoTelefone))
                transportadora.Telefone = novoTelefone;

            Console.Write($"Valor do frete atual: {transportadora.ValorFrete:C}. Novo valor: ");
            string novoValorStr = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoValorStr) && double.TryParse(novoValorStr, out double novoValor))
                transportadora.ValorFrete = novoValor;

            repositorio.Atualizar(transportadora);
            Console.WriteLine("Transportadora editada com sucesso!");
        }
    }
}
