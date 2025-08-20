using System;
using System.Collections.Generic;
using ProjetoLoja.Config;
using ProjetoLoja.Entidades;
using ProjetoLoja.Repositorios;
using ProjetoLoja.Interfaces;

namespace ProjetoLoja.Service
{
    public class UsuarioService
    {
        private List<Usuario> usuarios;
        private Configuracao Configuracao { get; set; }

        public UsuarioService(IClienteRepositorio clienteRepo)
        {
            usuarios = new List<Usuario>();
            Configuracao = new Configuracao();
            
            Configuracao.Formato = FormatoRetorno.Lista;

            usuarios.Add(new Administrador("admin", "123", "Administrador Padrão"));
            foreach (var c in clienteRepo.ListarTodos())
                usuarios.Add(c);
        }

        public Usuario Login()
        {
            Console.WriteLine("\n=== Login ===");
            Console.Write("Usuário: ");
            string user = Console.ReadLine();
            Console.Write("Senha: ");
            string pass = Console.ReadLine();

            foreach (var u in usuarios)
            {
                if (u.Autenticar(user, pass))
                {
                    Console.WriteLine($"\nBem-vindo, {u.NomeUsuario}!");
                    return u;
                }
            }

            Console.WriteLine("\nUsuário ou senha inválidos.");
            return null;
        }

        private void ExibirUsuarios()
        {
            if (Configuracao.Formato == FormatoRetorno.Vetor)
            {
                ImprimirVetor(usuarios.ToArray());
            }
            else
            {
                ImprimirLista(usuarios);
            }
        }

        private void ImprimirVetor(Usuario[] vetor)
        {
            Console.WriteLine("\n=== Usuários (vetor) ===");
            for (int i = 0; i < vetor.Length; i++)
                Console.WriteLine($"[{i}] {vetor[i].NomeUsuario}");
        }

        private void ImprimirLista(List<Usuario> lista)
        {
            Console.WriteLine("\n=== Usuários (lista) ===");
            foreach (var u in lista)
                Console.WriteLine($"- {u.NomeUsuario}");
        }
    }
}
