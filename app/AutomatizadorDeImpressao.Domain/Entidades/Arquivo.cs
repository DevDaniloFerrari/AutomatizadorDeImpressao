namespace AutomatizadorDeImpressao.Domain.Entidades
{
    public class Arquivo
    {
        public Arquivo(string nome, string diretorio, string extensao, int copias)
        {
            this.Nome = nome;
            this.Diretorio = diretorio;
            this.Extensao = extensao;
            this.Copias = copias;

            this.DiretorioCompleto = this.Diretorio + @"\" + this.Nome;
        }

        public string Nome { get; private set; }
        public string Diretorio { get; private set; }
        public string DiretorioCompleto { get; private set; }
        public string Extensao { get; private set; }
        public int Copias { get; private set; }

    }
}
