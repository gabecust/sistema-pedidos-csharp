using System;
using ProjetoLoja.Models;
using ProjetoLoja.Entidades;
using ProjetoLoja.Repositorios;
using ProjetoLoja.Service;

namespace ProjetoLoja
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu();
            menu.IniciarSistema();
        }
    }
}
