using AutomatizadorDeImpressao.Domain;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

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
            EventLog.WriteEntry("Iniciou o servico de Impressao", EventLogEntryType.Warning);

            ThreadStart start = new ThreadStart(IniciarServico);
            Thread thread = new Thread(start);

            thread.Start();

        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("Parou o servico de Impressao", EventLogEntryType.Warning);
        }

        public void IniciarServico()
        {
            using (var orquestrador = new Orquestrador())
            {
                orquestrador.Iniciar();
            }
        }
    }
}
