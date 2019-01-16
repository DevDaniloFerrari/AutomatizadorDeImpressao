using AutomatizadorDeImpressao.Domain.Entities;
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
        List<Arquivo> arquivos = new List<Arquivo>();

        public void Iniciar()
        {
            if (this.VerificarArquivos())
            {

            }
        }

        public bool VerificarArquivos()
        {
            string caminho = ConfigurationManager.AppSettings[Constantes.CAMINHO_DOS_ARQUIVOS];

            if (File.Exists(caminho))
            {
                return true;
            }

            return false;
        }


    }

}
