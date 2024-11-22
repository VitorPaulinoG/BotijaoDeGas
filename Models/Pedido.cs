public class Pedido {
    public static int UltimoCodigo { get; private set; } = 0;
    public int CodigoPedido { get; private set; }
    public DateTime DataCompra { get; set; }
    public string EnderecoEntrega { get; set; }
    public int QuantidadeBotijoes { get; set; }

    public decimal TotalCompra { 
        get {
            return 90.00M * QuantidadeBotijoes;
        }
    }
    public DateTime DataEntrega {
        get {
            return DataCompra.AddHours(6);
        }
    }

    public string CartaoCredito { get; set; }

    public string Status { get; set; }

    public Pedido(string enderecoEntrega, DateTime dataCompra)
    {
        EnderecoEntrega = enderecoEntrega;
        DataCompra = dataCompra;
        UltimoCodigo++;
        CodigoPedido = UltimoCodigo;

    }

}