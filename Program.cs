using System.Runtime.InteropServices;

IList<Pedido> pedidos = new List<Pedido>();

MostrarMenu ();


void MostrarMenu () 
{
    int opcao;
    do {
        Console.Clear();
        FormarPainel("MENU", [
            "1. Fazer Pedido",
            "2. Confirmar Entrega",
            "3. Ver Pedidos Confirmados",
            "4. Ver Pedidos Entregues",
            "5. Sair",
        ]);

        Console.WriteLine("Insira a opção desejada: ");
        opcao = int.Parse(Console.ReadLine());
        switch (opcao) {
            case 1: 
                FazerPedido();
            break;
            case 2: 
                ConfirmarEntrega();
            break;
            case 3: 
                MostrarPedidosPorStatus(StatusPedido.CONFIRMADO);
            break;
            case 4: 
                MostrarPedidosPorStatus(StatusPedido.ENTREGUE);
            break;
            case 5: 
                Console.WriteLine("Finalizando...");
            break;
            default:
                Console.WriteLine("Opção Inválida");
            break;
        }
    } while(opcao != 5);


}

void FazerPedido () 
{
    string endereco = string.Empty; 
    DateTime data = DateTime.Now;

    int opcao;
    do {
        Console.Clear();
        Console.WriteLine("Insira o endereço de entrega: ");
        endereco = Console.ReadLine();
        FormarPainel("Informações do Pedido", [
            $"Endereço de Entrega: {endereco}",
            $"Data e Hora da Compra: {data}"
        ]);
        Console.Write("(1) Confirmar ou (2) Alterar? ");
        opcao = int.Parse(Console.ReadLine());
    } while (opcao != 1);
    Console.Clear();
    Pedido pedido = new Pedido(endereco, data);
    Console.Write("Insira a quantidade de botijões: ");
    pedido.QuantidadeBotijoes = int.Parse(Console.ReadLine());
    
    FormarPainel("Informações do Pedido", [
        $"Endereço de Entrega: {endereco}",
        $"Data e Hora da Compra: {data}",
        $"Quantidade de Botijões: {pedido.QuantidadeBotijoes}",
        $"Total da Compra: R${pedido.TotalCompra:F2}",
        $"Data e Hora de Entrega: {pedido.DataEntrega}"
    ]);

    Console.WriteLine("Insira o Número do Cartão de Crédito: ");
    pedido.CartaoCredito = Console.ReadLine();
    Console.WriteLine($"Codigo do Pedido: {pedido.CodigoPedido}");
    pedido.Status = StatusPedido.CONFIRMADO;
    pedidos.Add(pedido);
}

void ConfirmarEntrega() 
{
    Console.WriteLine("Insira o Código do Pedido: ");
    int codigo = int.Parse(Console.ReadLine());
    Pedido? pedido = pedidos.FirstOrDefault(x => x.CodigoPedido == codigo);
    if(pedido is null) 
    {
        Console.WriteLine("Pedido não localizado.");
        return;
    }    
    pedido.Status = StatusPedido.ENTREGUE;
}

void MostrarPedidosPorStatus (StatusPedido status) 
{
    var pedidosEncontrados = pedidos.Where(x => x.Status == status);
    foreach (var pedido in pedidosEncontrados)
    {
        FormarPainel(pedido.CodigoPedido.ToString(), [
            $"Endereço de Entrega: {pedido.EnderecoEntrega}",
            $"Data e Hora da Compra: {pedido.DataCompra}",
            $"Quantidade de Botijões: {pedido.QuantidadeBotijoes}",
            $"Total da Compra: R${pedido.TotalCompra:F2}",
            $"Data e Hora de Entrega: {pedido.DataEntrega}"
        ]);
    }
    Console.ReadKey();
}

void FormarPainel (string titulo, params string[] itens) 
{
    int largura = itens.Max(x => x.Length) + 5;

    Console.Write("╭");
    for (int i = 0; i < largura; i++) 
    {
        Console.Write("─");
    }
    Console.WriteLine("╮");

    
    int padLeft = titulo.Length + (largura - titulo.Length)/2;
    int padRight = largura;

    Console.Write("│");
    Console.Write(titulo.PadLeft(padLeft, ' ').PadRight(largura, ' '));
    Console.WriteLine("│");
    
    Console.Write("├");
    for (int i = 0; i < largura; i++) 
    {
        Console.Write("─");
    }
    Console.WriteLine("┤");

    for (int i = 0; i < itens.Length; i++) 
    {
        Console.Write("│ ");
        Console.Write(itens[i]);
        for (int j = itens[i].Length + 1; j < largura; j++)
        {
            Console.Write(" ");
        }
        Console.WriteLine("│");

    }
    Console.Write("╰");
    for (int i = 0; i < largura; i++) 
    {
        Console.Write("─");
    }
    Console.WriteLine("╯");
}

