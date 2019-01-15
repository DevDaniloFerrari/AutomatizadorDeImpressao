namespace AutomatizadorDeImpressao.Domain.Entities
{
    public class Impressora
    {

        public Impressora(string nome, Arquivo arquivo)
        {
            this.nome = nome;
            this.quantidadeDeCopias = arquivo.copias;
        }

        public string nome { get; set; }
        public int quantidadeDeCopias { get; private set; }

    }
}
