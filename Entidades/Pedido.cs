using System.Text.Json.Serialization;
using ProjetoLoja.Models;
using ProjetoLoja.Entidades.Enums;
using ProjetoLoja.Entidades.Exceptions;

namespace ProjetoLoja.Entidades
{
    public class Pedido
    {
        [JsonInclude]
        public int Numero { get; private set; }
        public Cliente Cliente { get; set; }
        public DateTime Data { get; set; }
        public List<ItemPedido> Itens { get; set; }
        public Transportadora Transportadora { get; set; }

        public double TotalItens => CalcularTotalItens();
        public double Frete => Transportadora?.ValorFrete ?? 0;
        public double TotalGeral => TotalItens + Frete;

        public StatusPedido Status { get; set; }
        public DateTime? DataEnvio { get; private set; }
        public DateTime? DataCancelamento { get; private set; }

        public Pedido(){}

        public Pedido(int numero, Cliente cliente, List<ItemPedido> items, Transportadora transportadora)
        {
            if (numero <= 0)
                throw new PedidoInvalidoException("Número do pedido deve ser maior que zero.");
            
            if (cliente == null)
                throw new PedidoInvalidoException("Cliente não pode ser nulo.");
            
            if (items == null || items.Count == 0)
                throw new PedidoInvalidoException("Pedido deve conter pelo menos um item.");
            
            if (transportadora == null)
                throw new PedidoInvalidoException("Transportadora não pode ser nula.");

            Numero = numero;
            Cliente = cliente;
            Data = DateTime.Now;
            Itens = items;
            Transportadora = transportadora;
            Status = StatusPedido.Novo;
        }

        private double CalcularTotalItens()
        {
            double total = 0;
            foreach (var item in Itens)
            {
                total += item.Total;
            }
            return total;
        }

        public void MarcarComoEnviado()
        {
            Status = StatusPedido.Enviado;
            DataEnvio = DateTime.Now;
        }

        public void MarcarComoEntregue()
        {
            Status = StatusPedido.Entregue;
            DataEnvio = DateTime.Now;
        }

        public void Cancelar()
        {
            Status = StatusPedido.Cancelado;
            DataCancelamento = DateTime.Now;
        }

        public override string ToString()
        {
            string cabecalho = $"=== Pedido N {Numero} ===\n" +
                                $"Cliente: {Cliente.NomeCompleto} ({Cliente.NomeUsuario})\n" +
                                $"Data: {Data:dd/MM/yyyy HH:mm}\n" +
                                $"Status: {Status}";

            if (Status == StatusPedido.Enviado && DataEnvio.HasValue)
            {
                cabecalho += $"\nData de Envio: {DataEnvio:dd/MM/yyyy HH:mm}";
            }
            else if (Status == StatusPedido.Entregue && DataEnvio.HasValue)
            {
                cabecalho += $"\nData de Entrega: {DataEnvio:dd/MM/yyyy HH:mm}";
            }
            else if (Status == StatusPedido.Cancelado && DataCancelamento.HasValue)
            {
                cabecalho += $"\nData de Cancelamento: {DataCancelamento:dd/MM/yyyy HH:mm}";
            }

            string itens = "\n\n--- Itens do Pedido ---";
            foreach (var item in Itens)
            {
                itens += $"\n{item.Produto.Nome} | Quantidade: {item.Quantidade} | " +
                        $"Valor: R$ {item.Produto.Preco:F2} | Subtotal: R${item.Total:F2}";
            }

            string totais = $"\n\nFrete: R$ {Frete:F2}" +
                            $"\nTotal dos Itens: R${TotalItens:F2}" +
                            $"\nTotal Geral: R${TotalGeral:F2}";

            return $"{cabecalho}{itens}{totais}";
        }
    }

}

