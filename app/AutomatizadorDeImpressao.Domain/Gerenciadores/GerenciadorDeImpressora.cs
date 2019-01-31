using AutomatizadorDeImpressao.Domain.Entidades;
using GerenciadorDeLog.Domain.Entities;
using PdfiumViewer;
using System;
using System.Configuration;
using System.Drawing.Printing;
using System.Management;

namespace AutomatizadorDeImpressao.Domain.Gerenciadores
{
    public class GerenciadorDeImpressora
    {

        public static void CarregarImpressora(Impressora impressora)
        {

            GerenciadorDeLog.Domain.GerenciadorDeLog.Logar(Situacao.Informativo, String.Format("Carregando impressora {0}", impressora));

            string nomeDaImpressora = ConfigurationManager.AppSettings[Constantes.NOME_DA_IMPRESSORA];

            var printerQuery = new ManagementObjectSearcher(String.Format("SELECT * from Win32_Printer WHERE Name = '{0}'", nomeDaImpressora));

            foreach (var printer in printerQuery.Get())
            {

                string nome = Convert.ToString(printer.GetPropertyValue("Name"));
                string status = "";

                if (Convert.ToString(printer["WorkOffline"].ToString().ToLower()) == "false")
                {
                    status = "Ligada";
                }
                else
                {
                    status = "Desligada";
                }

                bool principal = Convert.ToBoolean(printer.GetPropertyValue("Default"));
                bool rede = Convert.ToBoolean(printer.GetPropertyValue("Network"));

                impressora.PreencherDados(nome, status, principal, rede);

            }

        }

        public static void ImprimirPDF(Impressora impressora, Arquivo arquivo)
        {

            GerenciadorDeLog.Domain.GerenciadorDeLog.Logar(Situacao.Inicializacao, "Inicializando a impressão");

            try
            {
                if (impressora.Status == "Ligada")
                {

                    var printerSettings = new PrinterSettings
                    {
                        PrinterName = impressora.Nome,
                        Copies = (short)arquivo.Copias,
                    };


                    var pageSettings = new PageSettings(printerSettings)
                    {
                        Margins = new Margins(0, 0, 0, 0),
                    };

                    foreach (PaperSize paperSize in printerSettings.PaperSizes)
                    {
                        if (paperSize.PaperName == arquivo.Nome)
                        {
                            pageSettings.PaperSize = paperSize;
                            break;
                        }
                    }


                    using (var document = PdfDocument.Load((arquivo.Diretorio + @"\" + arquivo.Nome)))
                    {
                        using (var printDocument = document.CreatePrintDocument())
                        {
                            printDocument.PrinterSettings = printerSettings;
                            printDocument.DefaultPageSettings = pageSettings;
                            printDocument.PrintController = new StandardPrintController();

                            GerenciadorDeLog.Domain.GerenciadorDeLog.Logar(Situacao.Informativo, String.Format("Imprimindo o arquivo {0}", arquivo.Nome));

                            printDocument.Print();

                            document.Dispose();

                            GerenciadorDeArquivos.Mover(arquivo);
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                GerenciadorDeLog.Domain.GerenciadorDeLog.Logar(Situacao.Excecao, exception.Message);
            }
        }

    }
}
