using AutomatizadorDeImpressao.Domain.Entidades;
using AutomatizadorDeImpressao.Domain.Gerenciadores;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatizadorDeImpressao.Domain
{
    public class Orquestrador
    {
        private static List<Arquivo> arquivos = new List<Arquivo>();
        private static DirectoryInfo directoryInfo = null;
        private static FileInfo[] Files = null;
        private static Impressora impressora = GerenciadorDeImpressora.ObterImpressora(ConfigurationManager.AppSettings[Constantes.NOME_DA_IMPRESSORA]);

        public void Iniciar()
        {
            this.VerificarArquivos();

            if (arquivos.Count != 0)
            {
                foreach (Arquivo arquivo in arquivos)
                {
                    GerenciadorDeImpressora.ImprimirPDF(impressora, arquivo);
                }

            }

        }

        public List<Arquivo> VerificarArquivos()
        {
            string caminho = ConfigurationManager.AppSettings[Constantes.CAMINHO_DOS_ARQUIVOS];

            directoryInfo = new DirectoryInfo(caminho);
            Files = directoryInfo.GetFiles("*IMPRIMIR*");


            if (Files.Length != 0)
            {
                foreach (FileInfo fileInfo in Files)
                {
                    if (fileInfo.Extension == ".pdf")
                        GerenciadorDeArquivos.AdicionarArquivo(arquivos, fileInfo);
                }

            }

            return arquivos;
        }


    }

}
