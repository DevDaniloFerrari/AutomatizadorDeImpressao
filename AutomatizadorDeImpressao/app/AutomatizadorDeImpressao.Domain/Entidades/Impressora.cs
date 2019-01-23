namespace AutomatizadorDeImpressao.Domain.Entidades
{
    public class Impressora
    {

        public Impressora()
        {
        }

        public string Nome { get; private set; }
        public string Status { get; private set; }
        public bool Principal { get; private set; }
        public bool Rede { get; private set; }

        public void PreencherDados(string nome, string status, bool principal, bool rede)
        {
            this.Nome = nome;
            this.Status = status;
            this.Principal = principal;
            this.Rede = rede;
        }
    }
}
