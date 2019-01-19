using AutomatizadorDeImpressao.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace AutomatizadorDeImpressao.Domain.Gerenciadores
{
    public class GerenciadorDeArquivos
    {

        private static Arquivo arquivo = null;

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
                return 0;
            }

        }

        public static void Mover(Arquivo arquivo)
        {
            string pasta = ConfigurationManager.AppSettings[Constantes.CAMINHO_DOS_ARQUIVOS_IMPRESSOS]
                           +  @"\" +
                           ConfigurationManager.AppSettings[Constantes.NOME_DA_PASTA_DE_ARQUIVOS_IMPRESSOS];

            Directory.CreateDirectory(pasta);

            File.Move((arquivo.Diretorio+@"\"+arquivo.Nome),(pasta+@"\"+arquivo.Nome));

        }
    }
}
