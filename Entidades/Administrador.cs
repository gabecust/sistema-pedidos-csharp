using System;

namespace ProjetoLoja.Entidades
{
    public class Administrador : Usuario
    {
        public string Nome { get; set; }

        public Administrador(string nomeUsuario, string senha, string nome)
            : base(nomeUsuario, senha)
        {
            Nome = nome;
        }

        public override string Tipo => "Administrador";

        public override string ToString()
        {
            return $"{Nome} | Usuário: {NomeUsuario} (Administrador)";
        }
    }
}
