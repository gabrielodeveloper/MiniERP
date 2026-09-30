using AcessoBancoDados;
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
                acessoDadosSqlServer.AdicionarParametros("@IDPessoa", cliente.IDPessoaCliente);
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
                acessoDadosSqlServer.AdicionarParametros("@IDPessoa", codigo);
                acessoDadosSqlServer.AdicionarParametros("@Nome", nomeCliente);

                DataTable dataTableCliente = acessoDadosSqlServer.ExecutarConsulta(CommandType.StoredProcedure, "uspConsultarClientePorCodigoOuNome");

                foreach (DataRow row in dataTableCliente.Rows)
                {
                    Cliente cliente = new Cliente();

                    cliente.IDPessoaCliente = Convert.ToInt32(row["IDPessoaCliente"]);
                    cliente.CPFCNPJ = Convert.ToString(row["CPFCNPJ"]);
                    cliente.Nome = Convert.ToString(row["Nome"]);
                    cliente.IDPessoaTipo = Convert.ToInt32(row["IDPessoaTipo"]);
                    cliente.TipoPessoa = Convert.ToString(row["TipoPessoa"]);

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
