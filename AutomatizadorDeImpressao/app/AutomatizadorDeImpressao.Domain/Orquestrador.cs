using AutomatizadorDeImpressao.Domain.Entidades;
using AutomatizadorDeImpressao.Domain.Gerenciadores;
using System;
using System.Collections.Generic;

namespace AutomatizadorDeImpressao.Domain
{
    public class Orquestrador : IDisposable
    {
        private static List<Arquivo> arquivos = new List<Arquivo>();
        private static Impressora impressora = new Impressora();

        

        public void Iniciar()
        {
            GerenciadorDeArquivos.CarregarArquivos(arquivos);

            if (arquivos.Count != 0)
            {
                foreach (Arquivo arquivo in arquivos)
                {
                    GerenciadorDeImpressora.CarregarImpressora(impressora);
                    GerenciadorDeImpressora.ImprimirPDF(impressora, arquivo);
                }

            }

            arquivos.Clear();

        }


        public void Dispose()
        {
            //Nao precisa fazer nada
        }

    }

}
