namespace ProjetoLoja.Models;

public class Endereco
{
    public string Rua { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cep { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }

    public Endereco(string rua, string numero, string complemento, string bairro, string cep, string cidade, string estado)
    {
        Rua = rua;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        Cep = cep;
        Cidade = cidade;
        Estado = estado;
    }

    public override string ToString()
    {
        return $"{Rua}, {Numero} - {Bairro} - {Cidade}/{Estado} - CEP: {Cep}";
    }
}
