namespace ProjetoLoja.Config
{
    public enum FormatoRetorno { Vetor = 1, Lista = 2 }
    public class Configuracao
    {
        public FormatoRetorno Formato { get; set; } = FormatoRetorno.Vetor;
    }
}
