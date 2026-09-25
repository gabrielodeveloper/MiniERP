using AcessoBancoDados;
using ObjetoTransferencia;
using System;
using System.Data;

namespace Negocios
{
    public class PedidoNegocio
    {
        private readonly AcessoDadosSqlServer acessoDadosSqlServer;

        public PedidoNegocio(AcessoDadosSqlServer acessoDadosSqlServer)
        {
            this.acessoDadosSqlServer = acessoDadosSqlServer;
        }
        public string InserirPedido(Pedido pedido)
        {
            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@IDCliente", pedido.Cliente.IDCliente);
                string IDPedido = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspCadastrarPedido").ToString();

                return IDPedido;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível inserir pedido, Detalhes: {ex.Message}"); ;
            }
        }

        public PedidoColacao ConsultarPedido(int idPedido)
        {
            try
            {
                PedidoColacao pedidos = new PedidoColacao();

                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@IDPedido", idPedido);
                DataTable dataTablePedidos = acessoDadosSqlServer.ExecutarConsulta(CommandType.StoredProcedure, "uspConsultarPedido");

                foreach (DataRow row in dataTablePedidos.Rows)
                {
                    Pedido pedido = new Pedido();
                

                    pedido.IDPedido = Convert.ToInt32(row["IDPedido"]);
                    pedido.DataHora = Convert.ToDateTime(row["DataHora"]);
                    pedido.Cliente = new Cliente();

                    pedido.Cliente.IDCliente = Convert.ToInt32(row["IDCliente"]);
                    pedido.Cliente.Nome = Convert.ToString(row["Nome"]);
                    pedido.Cliente.CPF = Convert.ToString(row["CPF"]);
                    pedido.QuantidadeTotal = Convert.ToInt32(row["QuantidadeTotal"]);
                    pedido.ValorTotal = Convert.ToDecimal(row["ValorTotal"]);

                    pedidos.Add(pedido);
                }

                return pedidos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível consultar pedido, Detalhes: {ex.Message}");
            }
        }

        public string CancelarPedido(int idPedido)
        {
            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@IDPedido", idPedido);
                string idPedidoCancelado = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspCancelarPedido").ToString();

                return idPedidoCancelado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível cancelar o pedido. Detalhe: {ex.Message}");
            }
        }
    }
}
