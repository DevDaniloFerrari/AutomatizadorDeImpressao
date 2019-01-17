using AutomatizadorDeImpressao.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            arquivo = new Arquivo(nome,diretorio,extensao,copias);

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
    }
}
