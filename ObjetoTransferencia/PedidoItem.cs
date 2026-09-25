using System;

namespace ObjetoTransferencia
{
    public class PedidoItem
    {
        public int IDPedidoItem { get; set; }
        public int IDPedido { get; set; }
        public Produto Produto { get; set; } = new Produto();
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
