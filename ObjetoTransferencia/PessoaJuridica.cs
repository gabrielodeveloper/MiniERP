namespace ObjetoTransferencia
{
    public class PessoaJuridica
    {
        public int IDPessoaJuridica { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string? InscricaoEstadual { get; set; }
        public DateTime DataFundacao { get; set; }
    }
}
