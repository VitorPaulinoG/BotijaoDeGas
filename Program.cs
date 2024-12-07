

IList<Pedido> pedidos = new List<Pedido>();

MostrarMenu ();


void MostrarMenu () 
{
    int opcao = 0;
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
        try 
        {
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
        } catch (FormatException) {
            Console.WriteLine("Você precisa digitar um número inteiro para definir a opção!");
            Console.ReadKey();
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

    try {
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

    } catch (FormatException) {
        Console.WriteLine("A quantidade de botijões deve ser inteira!");
        Console.ReadKey();
    }
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
    var pedidosEncontrados = pedidos.Where(x => x.Status == status).ToList();
    if(pedidosEncontrados.Any()) {
        FormarTabela(pedidosEncontrados);
    } else {
        Console.WriteLine($"Nenhum pedido {status} encontrado");
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

void FormarTabela (IList<Pedido> pedidos) 
{
    int[] widths = [
        pedidos.Max(x => x.CodigoPedido.ToString().Count()) + 2,
        pedidos.Max(x => x.EnderecoEntrega.Length) + 2,
        19 + 2,
        pedidos.Max(x => x.QuantidadeBotijoes.ToString().Length) + 2,
        pedidos.Max(x => x.TotalCompra.ToString().Length),
        19 + 2
    ];

    string[] headers = ["cod", "endereço", "data de compra", "qtd", "total", "data de entrega"];
    
    int maxWidth = widths.Sum() + widths.Length + 1;
    
    
    Console.Write("╭");
    for (int i = 0; i < widths.Length; i++) {
        Console.Write(string.Concat(Enumerable.Repeat('─', widths[i])));
        if(i < widths.Length - 1)
            Console.Write("┬");
    }
    Console.WriteLine("╮");

    Console.Write("│");
    for (int i = 0; i < widths.Length; i++) {
        Console.Write(headers[i]);
        Console.Write(string.Concat(Enumerable.Repeat(' ', Math.Max(widths[i] - headers[i].Length, 0))));
        Console.Write("│");
    }
    Console.WriteLine();

    Console.Write("├");
    for (int i = 0; i < widths.Length; i++) {
        Console.Write(string.Concat(Enumerable.Repeat('─', widths[i])));
        if(i < widths.Length - 1)
            Console.Write("┼");
    }
    Console.WriteLine("┤");
    
    for (int i = 0; i < pedidos.Count(); i++)
    {
        string[] props = pedidos[i].ToString().Split(",\n");
        

        Console.Write("│");
        for (int j = 0; j < widths.Length; j++) {
            
            Console.Write($"{props[j]}");

            int space = widths[j] - props[j].Length;
            if(space > 0)
                Console.Write(string.Concat(Enumerable.Repeat(' ', space)));
            Console.Write("│");
        }
        Console.WriteLine();

        if(i < pedidos.Count() - 1) {
            Console.Write("├");
            for (int j = 0; j < widths.Length; j++) {
                Console.Write(string.Concat(Enumerable.Repeat('─', widths[j])));
                if(j < widths.Length - 1)
                    Console.Write("┼");
            }
            Console.WriteLine("┤");
        } else {
            Console.Write("╰");
            for (int j = 0; j < widths.Length; j++) {
                Console.Write(string.Concat(Enumerable.Repeat('─', widths[j])));
                if(j < widths.Length - 1)
                    Console.Write("┴");
            }
            Console.WriteLine("╯");
        }

        
    }
}


