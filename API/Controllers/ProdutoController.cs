using Microsoft.AspNetCore.Mvc;
using Negocios;
using ObjetoTransferencia;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoNegocio produtoNegocio;

        public ProdutoController(ProdutoNegocio produtoNegocio)
        {
            this.produtoNegocio = produtoNegocio;
        }

        [HttpGet]
        public IActionResult Consultar(int? idProduto, string? descricao)
        {
            var produto = produtoNegocio.ConsultarProduto(idProduto, descricao);

            return Ok(produto);
        }

        [HttpPost]
        public IActionResult Inserir([FromBody] Produto produto)
        {
            try
            {
                string idProduto = produtoNegocio.InserirProduto(produto);

                return StatusCode(201, new
                {
                    idProduto = idProduto
                });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Este produto já possui cadastro")){
                    return Conflict(new
                    {
                        mensagem = "Este produto já possui cadastro"
                    });
                }

                return StatusCode(500, new
                {
                    mensagem = "Ocorreu um erro interno ao cadastrar o produto."
                });
            }
        }
    }
}
