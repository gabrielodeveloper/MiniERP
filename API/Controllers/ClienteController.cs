using Microsoft.AspNetCore.Mvc;
using Negocios;
using ObjetoTransferencia;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteNegocio clienteNegocio;
        public ClienteController(ClienteNegocio clienteNegocio)
        {
            this.clienteNegocio = clienteNegocio;
        }

        [HttpGet]
        public IActionResult Consultar(int? idCliente, string? nome)
        {
            var clientes = clienteNegocio.ConsultarClientePorCodigoOuNome(idCliente, nome);

            return Ok(clientes);    
        }

        [HttpPost]
        public IActionResult Inserir([FromBody] Cliente cliente)
        {
            try
            {
                string idCliente = clienteNegocio.InserirCliente(cliente);

                return StatusCode(201, new
                {
                    idCliente = idCliente
                });

            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("Este cliente já possui cadastro")){
                    return Conflict(new
                    {
                        mensagem = "Este cliente já possui cadastro"
                    });
                }
    
                return StatusCode(500, new
                {
                    mensagem = "Ocorreu um erro interno ao cadastrar o cliente."
                });
            }
        }
    }
}

