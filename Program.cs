IList<Pedido> pedidos = new List<Pedido>();

MostrarMenu ();


void MostrarMenu () 
{
    FormarPainel("MENU", [
        "1. Fazer Pedido",
        "2. Confirmar Entrega",
        "3. Ver Pedidos Confirmados",
        "4. Ver Pedidos Entregues",
        "5. Sair",
    ]);
}

void FazerPedido () 
{
    string endereco = string.Empty; 
    DateTime data = DateTime.Now;

    int opcao;
    do {
        Console.WriteLine("Insira o endereço de entrega: ");
        endereco = Console.ReadLine();
        FormarPainel("Informações do Pedido", [
            $"Endereço: {endereco}",
            $"Data e Hora da Compra: {data.TimeOfDay}"
        ]);
        Console.Write("(1) Confirmar ou (2) Alterar? ");
        opcao = int.Parse(Console.ReadLine());


    } while (opcao != 1);

    Pedido pedido = new Pedido(endereco, data);
    Console.Write("Insira a quantidade de botijões: ");
    pedido.QuantidadeBotijoes = int.Parse(Console.ReadLine());
    
    FormarPainel("Informações do Pedido", [
        $"Endereço: {endereco}",
        $"Data e Hora da Compra: {data.TimeOfDay}",
        $"Quantidade de Botijões: {pedido.QuantidadeBotijoes}",
        $"Total da Compra: R${pedido.TotalCompra:F2}",
        $"Data e Hora de Entrega: {pedido.DataEntrega}"
    ]);

    
    

    pedidos.Add(pedido);
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

