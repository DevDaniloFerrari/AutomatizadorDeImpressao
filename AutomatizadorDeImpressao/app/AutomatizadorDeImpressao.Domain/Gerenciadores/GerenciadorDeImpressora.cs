using AutomatizadorDeImpressao.Domain.Entidades;
using PdfiumViewer;
using System;
using System.Drawing.Printing;
using System.Management;

namespace AutomatizadorDeImpressao.Domain.Gerenciadores
{
    public class GerenciadorDeImpressora
    {

        public static Impressora ObterImpressora(string nomeDaImpressora)
        {
            var printerQuery = new ManagementObjectSearcher(String.Format("SELECT * from Win32_Printer WHERE Name = '{0}'", nomeDaImpressora));

            Impressora impressora;

            foreach (var printer in printerQuery.Get())
            {

                string nome = Convert.ToString(printer.GetPropertyValue("Name"));
                string status = Convert.ToString(printer.GetPropertyValue("Status"));
                bool principal = Convert.ToBoolean(printer.GetPropertyValue("Default"));
                bool rede = Convert.ToBoolean(printer.GetPropertyValue("Network"));

                impressora = new Impressora(nome, status, principal, rede);

                return impressora;
            }

            return null;

        }

        public static void ImprimirPDF(Impressora impressora, Arquivo arquivo)
        {
            try
            {

                // Create the printer settings for our printer
                var printerSettings = new PrinterSettings
                {
                    PrinterName = impressora.Nome,
                    Copies = (short)arquivo.Copias,
                };

                // Create our page settings for the paper size selected
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

                // Now print the PDF document
                using (var document = PdfDocument.Load((arquivo.Diretorio + @"\" + arquivo.Nome)))
                {
                    using (var printDocument = document.CreatePrintDocument())
                    {
                        printDocument.PrinterSettings = printerSettings;
                        printDocument.DefaultPageSettings = pageSettings;
                        printDocument.PrintController = new StandardPrintController();
                        printDocument.Print();

                        document.Dispose();

                        GerenciadorDeArquivos.Mover(arquivo);
                    }
                }

            }
            catch(Exception e)
            {

            }
        }

    }
}
