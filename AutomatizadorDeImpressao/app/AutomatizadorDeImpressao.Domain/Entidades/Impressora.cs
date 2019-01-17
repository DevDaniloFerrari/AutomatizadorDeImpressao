namespace AutomatizadorDeImpressao.Domain.Entidades
{
    public class Impressora
    {

        public Impressora(string nome, Arquivo arquivo)
        {
            this.Nome = nome;
            this.QuantidadeDeCopias = arquivo.Copias;
        }

        public string Nome { get; set; }
        public int QuantidadeDeCopias { get; private set; }

    }
}
