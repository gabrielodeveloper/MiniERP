namespace ObjetoTransferencia
{
    public class Cliente
    {
        public int IDPessoaCliente { get; set; }
        public int IDPessoaTipo { get; set; }
        public string TipoPessoa { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string CPFCNPJ { get; set; } = string.Empty;
    }
}
