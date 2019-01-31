using AutomatizadorDeImpressao.Domain.Entidades;
using AutomatizadorDeImpressao.Domain.Gerenciadores;
using System;
using System.Collections.Generic;
using System.Configuration;

using GerenciadorDeLog.Domain.Entities;

namespace AutomatizadorDeImpressao.Domain
{
    public class Orquestrador : IDisposable
    {
        private static List<Arquivo> arquivos = new List<Arquivo>();
        private static Impressora impressora = new Impressora();



        public void Iniciar()
        {
            GerenciadorDeLog.Domain.GerenciadorDeLog.ConfigurarCaminhoDoLog(ConfigurationManager.AppSettings[Constantes.CAMINHO_DO_LOG]);

            GerenciadorDeArquivos.CarregarArquivos(arquivos);

            GerenciadorDeLog.Domain.GerenciadorDeLog.Logar(Situacao.Informativo, String.Format("{0} arquivos encontrados na pasta", arquivos.Count));

            if (arquivos.Count != 0)
            {
                foreach (Arquivo arquivo in arquivos)
                {
                    GerenciadorDeImpressora.CarregarImpressora(impressora);
                    GerenciadorDeImpressora.ImprimirPDF(impressora, arquivo);
                }

            }

            GerenciadorDeLog.Domain.GerenciadorDeLog.Logar(Situacao.Finalizacao, "Limpando a lista de impressão");
            arquivos.Clear();

        }


        public void Dispose()
        {
            //Nao precisa fazer nada
        }

    }

}
