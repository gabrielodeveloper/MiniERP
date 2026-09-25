using Microsoft.AspNetCore.Mvc;
using Negocios;
using ObjetoTransferencia;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoNegocio pedidoNegocio;

        public PedidoController(PedidoNegocio pedidoNegocio)
        {
            this.pedidoNegocio = pedidoNegocio;
        }

        [HttpGet]
        public IActionResult Consultar(int idPedido)
        {
            var pedido = pedidoNegocio.ConsultarPedido(idPedido);

            return StatusCode(200, pedido);
        }

        [HttpPost]
        public IActionResult Inserir([FromBody] Pedido pedido)
        {
            try
            {
                var idPedido = pedidoNegocio.InserirPedido(pedido);

                return StatusCode(201, new
                {
                    idPedido = idPedido
                });

            }
            catch (Exception ex)
            {
                
                 throw new Exception($"Não foi possível inserir o pedido, Detalhes: {ex.Message}");
            }
        }

        [HttpPut("{idPedido}")]
        public IActionResult Cancelar([FromRoute] int idPedido)
        {
            var pedido = pedidoNegocio.CancelarPedido(idPedido);

            return StatusCode(200, idPedido);
        }
    }
}
