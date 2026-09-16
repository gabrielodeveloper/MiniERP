using AcessoBancoDados;
using Microsoft.Data.SqlClient;
using ObjetoTransferencia;
using System.Data;

namespace Negocios
{
    public class ClienteNegocio
    {
        private readonly AcessoDadosSqlServer acessoDadosSqlServer;
        
        public ClienteNegocio(AcessoDadosSqlServer acessoDadosSqlServer)
        {
            this.acessoDadosSqlServer = acessoDadosSqlServer;
        }
        public string InserirCliente(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@Nome", cliente.Nome);
                acessoDadosSqlServer.AdicionarParametros("@CPF", cliente.CPF);
                string IDCliente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspCadastrarCliente").ToString();

                return IDCliente;

            }
            catch (Exception ex)
            {

                throw new Exception($"Não foi possível inserir cliente, Detalhes: {ex.Message}"); ;
            }
        }

        public ClienteColecao ConsultarClientePorCodigoOuNome(int? idCliente, string nome)
        {
            try
            {
                ClienteColecao clienteColecao = new ClienteColecao();

                object codigo = idCliente ?? (object)DBNull.Value;
                object nomeCliente = string.IsNullOrEmpty(nome) ? (object)DBNull.Value : nome;

                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@IDCliente", codigo);
                acessoDadosSqlServer.AdicionarParametros("@Nome", nomeCliente);

                DataTable dataTableCliente = acessoDadosSqlServer.ExecutarConsulta(CommandType.StoredProcedure, "uspConsultarClientePorCodigoOuNome");

                foreach (DataRow row in dataTableCliente.Rows)
                {
                    Cliente cliente = new Cliente();

                    cliente.IDCliente = Convert.ToInt32(row["IDCliente"]);
                    cliente.Nome = Convert.ToString(row["Nome"]);
                    cliente.CPF = Convert.ToString(row["CPF"]);
                    cliente.Ativo = Convert.ToBoolean(row["Ativo"]);

                    clienteColecao.Add(cliente);
                }

                return clienteColecao;
            }
            catch (Exception ex)
            {

                throw new Exception($"Não foi possível consultar cliente, Detalhes: {ex.Message}");
            }
        }
    }
}
