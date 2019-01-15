namespace AutomatizadorDeImpressao.Domain.Entities
{
    public class Arquivo
    {
        public Arquivo(string nome, string local, string extensão, int copias)
        {
            this.nome = nome;
            this.local = local;
            this.extensão = extensão;
            this.copias = copias;
        }

        public string nome { get; private set; }
        public string local { get; private set; }
        public string extensão { get; private set; }
        public int copias { get; private set; }

    }
}
