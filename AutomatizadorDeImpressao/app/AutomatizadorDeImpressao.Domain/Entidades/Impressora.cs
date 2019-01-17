namespace AutomatizadorDeImpressao.Domain.Entidades
{
    public class Impressora
    {
        public Impressora(string nome, string status, bool principal, bool rede)
        {
            Nome = nome;
            Status = status;
            Principal = principal;
            Rede = rede;
        }

        public string Nome { get; private set; }
        public string Status { get; private set; }
        public bool Principal { get; private set; }
        public bool Rede { get; private set; }

    }
}
