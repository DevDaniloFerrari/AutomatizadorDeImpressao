using AutomatizadorDeImpressao.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AutomatizadorDeImpressao.UI.WindowsService
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            using (Orquestrador orquestrador = new Orquestrador())
            {
                orquestrador.Iniciar();
            }
        }

        protected override void OnStop()
        {
        }
    }
}
