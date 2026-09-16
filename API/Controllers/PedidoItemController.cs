using Microsoft.AspNetCore.Mvc;
using Negocios;
using ObjetoTransferencia;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoItemController : ControllerBase
    {
        private readonly PedidoItemNegocio pedidoItemNegocio;
        public PedidoItemController(PedidoItemNegocio pedidoItemNegocio)
        {
            this.pedidoItemNegocio = pedidoItemNegocio;
        }

        [HttpGet]
        public IActionResult Consultar(int idPedido) 
        { 
            var itens = pedidoItemNegocio.ConsultarPedidoItem(idPedido);

            return Ok(itens);
        }
        [HttpPost]
        public IActionResult Inserir(PedidoItem pedidoItem)
        {
            var item = pedidoItemNegocio.InserirPedidoItem(pedidoItem);

            return Ok(item);
        }
        [HttpPut]
        public IActionResult Alterar(PedidoItem pedidoItem)
        {
            var item = pedidoItemNegocio.AlterarPedidoItem(pedidoItem);

            return  Ok(item);
        }

        [HttpDelete("{idPedidoItem}")]
        public IActionResult Excluir(int idPedidoItem)
        {
            var item = pedidoItemNegocio.ExcluirPedidoitem(
                new PedidoItem { IDPedidoItem = idPedidoItem }
                );

            return Ok(item);
        }
    }
}
