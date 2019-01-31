using AutomatizadorDeImpressao.Domain.Entidades;
using GerenciadorDeLog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace AutomatizadorDeImpressao.Domain.Gerenciadores
{
    public class GerenciadorDeArquivos
    {

        private static Arquivo arquivo = null;
        private DirectoryInfo directoryInfo;

        public FileInfo[] Files { get; private set; }

        public static void AdicionarArquivo(List<Arquivo> arquivos, FileInfo fileInfo)
        {

            string nome = fileInfo.Name;
            string diretorio = fileInfo.DirectoryName;
            string extensao = fileInfo.Extension;
            int copias = ObterQuantidadeDeCopias(fileInfo);

            arquivo = new Arquivo(nome, diretorio, extensao, copias);

            arquivos.Add(arquivo);
        }

        private static int ObterQuantidadeDeCopias(FileInfo fileInfo)
        {
            try
            {
                string nome = fileInfo.Name;
                nome = nome.Substring(0, nome.LastIndexOf("."));
                nome = nome.Substring(nome.Length - 2, 1);

                return Convert.ToInt16(nome);
            }
            catch (FormatException formatException)
            {
                GerenciadorDeLog.Domain.GerenciadorDeLog.Logar(Situacao.Excecao, formatException.Message);
                return 0;
            }

        }

        public static void Mover(Arquivo arquivo)
        {

            GerenciadorDeLog.Domain.GerenciadorDeLog.Logar(Situacao.Informativo, String.Format("Movendo o arquivo {0}", arquivo.Nome));

            string pasta = ConfigurationManager.AppSettings[Constantes.CAMINHO_DOS_ARQUIVOS_IMPRESSOS]
                           + @"\" +
                           ConfigurationManager.AppSettings[Constantes.NOME_DA_PASTA_DE_ARQUIVOS_IMPRESSOS];

            Directory.CreateDirectory(pasta);

            File.Move((arquivo.DiretorioCompleto), (pasta + @"\" + arquivo.Nome));

        }

        public static void CarregarArquivos(List<Arquivo> arquivos)
        {

            GerenciadorDeLog.Domain.GerenciadorDeLog.Logar(Situacao.Informativo, "Carregando arquivos");

            string caminho = ConfigurationManager.AppSettings[Constantes.CAMINHO_DOS_ARQUIVOS];

            DirectoryInfo directoryInfo = new DirectoryInfo(caminho);
            FileInfo[] Files = directoryInfo.GetFiles("*IMPRIMIR*");

            if (Files.Length != 0)
            {
                foreach (FileInfo fileInfo in Files)
                {
                    if (fileInfo.Extension == ".pdf")
                        GerenciadorDeArquivos.AdicionarArquivo(arquivos, fileInfo);
                }

            }

        }

    }
}
