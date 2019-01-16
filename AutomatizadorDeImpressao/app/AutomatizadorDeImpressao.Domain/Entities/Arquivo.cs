namespace AutomatizadorDeImpressao.Domain.Entities
{
    public class Arquivo
    {
        public Arquivo(string nome, string diretorio, string extensao, int copias)
        {
            this.Nome = nome;
            this.Diretorio = diretorio;
            this.Extensao = extensao;
            this.Copias = copias;
        }

        public string Nome { get; private set; }
        public string Diretorio { get; private set; }
        public string Extensao { get; private set; }
        public int Copias { get; private set; }

    }
}
